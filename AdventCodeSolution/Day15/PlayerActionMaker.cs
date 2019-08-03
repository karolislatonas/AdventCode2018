using AdventCodeSolution.Day10;
using AdventCodeSolution.Day15.Players;
using AdventCodeSolution.Day03;
using AdventCodeSolution.Optionable;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AdventCodeSolution.Day15
{
    public class PlayerActionMaker
    {
        private readonly BattleMap map;

        public PlayerActionMaker(BattleMap map)
        {
            this.map = map;
        }

        public void MakeActionForPlayer(Player player)
        {
            if (player.IsDead)
                return;

            var maybeEnemy = FindEnemyInRangeToAttack(player);

            maybeEnemy
                .WhenSome(player.Attack)
                .WhenNone(() =>
                {
                    MovePLayer(player);
                    FindEnemyInRangeToAttack(player).WhenSome(player.Attack);
                });
        }

        private void MovePLayer(Player player)
        {
            FindShortestPathToEnemy(player)
                .WhenSome(path => player.MoveTo(path.First()));
        }

        private Maybe<Player> FindEnemyInRangeToAttack(Player player)
        {
            return map
                .GetEnemiesInRange(player)
                .NoneIfEmpty(es => es.MinBy(e => e.HitPoints));
        }

        private Maybe<XY[]> FindShortestPathToEnemy(Player player)
        {
            var initialPath = ImmutableList.Create(player.Location);
            var currentPaths = new List<ImmutableList<XY>>(initialPath.RepeatOnce());

            var reachedLocations = new HashSet<XY>(currentPaths.SelectMany(p => p));
            bool reachedLocationsChanged;

            do
            {
                var previousReachedLocationsCount = reachedLocations.Count;
                var newPaths = new List<ImmutableList<XY>>();

                var expandedPaths = currentPaths.SelectMany(p => ExpandPathToUndiscoveredInRangeLocations(player, p, reachedLocations));
                var enemiesReachedPaths = new List<IList<XY>>();
                foreach (var path in expandedPaths)
                {
                    if (PathReachedEnemy(path, player))
                    {
                        enemiesReachedPaths.Add(path);
                    }

                    reachedLocations.Add(path[path.Count - 1]);
                    newPaths.Add(path);
                }

                if(enemiesReachedPaths.Count > 0)
                {
                    return PickPathToEnemyFromMultiple(enemiesReachedPaths).Skip(1).ToArray().Some();
                }

                currentPaths = newPaths;
                reachedLocationsChanged = previousReachedLocationsCount != reachedLocations.Count;

            } while (reachedLocationsChanged);

            return None.Value;
        }

        private IList<XY> PickPathToEnemyFromMultiple(List<IList<XY>> enemiesReachedPaths)
        {
            return enemiesReachedPaths.MinBy(p => p[p.Count - 1], FromLeftHighestToRightLowestComparer.Value);
        }

        private bool PathReachedEnemy(IList<XY> path, Player player)
        {
            var lastLocation = path[path.Count - 1];
            var possibleEnemy = map.GetPlayerAtLocationOrDefault(lastLocation);

            return possibleEnemy?.IsEnemy(player) == true;
        }

        private IEnumerable<ImmutableList<XY>> ExpandPathToUndiscoveredInRangeLocations(Player player, ImmutableList<XY> originalPath, HashSet<XY> discoveredLocations)
        {
            var inRangeLocations = map.GetInRangeLocations(originalPath[originalPath.Count - 1])
                .Where(l => !discoveredLocations.Contains(l))
                .Where(l => !map.IsLocationOccupied(l) || map.GetPlayerAtLocationOrDefault(l).IsEnemy(player))
                .ToArray();

            if (inRangeLocations.Length == 0)
                return Enumerable.Empty<ImmutableList<XY>>();

            return inRangeLocations.Select(r => originalPath.Add(r));
        }
    }
}
