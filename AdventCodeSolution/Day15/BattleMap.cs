using AdventCodeSolution.Day10;
using AdventCodeSolution.Day15.Players;
using AdventCodeSolution.Day3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventCodeSolution.Day15
{
    public class BattleMap
    {
        private readonly HashSet<XY> wallLocations;
        private readonly SortedDictionary<XY, Player> playersByLocation;

        public BattleMap(IEnumerable<XY> wallLocations, IEnumerable<Player> players)
        {
            this.wallLocations = wallLocations.ToHashSet();
            playersByLocation = players.ToSortedDictionary(p => p.Location, p => p, FromLeftHighestToRightLowestComparer.Value);

            foreach(var player in playersByLocation.Values)
            {
                player.PlayerDied += OnPlayerDied;
                player.PlayerMoved += OnPlayerMoved;
            }
        }

        public IEnumerable<Player> GetEnemiesOf(Player player) => Players.Where(player.IsEnemy);

        public Player[] GetPlayerOfRace<T>()
            where T : Player
        {
            return Players.OfType<T>().ToArray();
        }

        public IEnumerable<XY> GetInRangeLocations(XY location) => EnumerateInRangeLocations(location).Where(l => !wallLocations.Contains(l));

        public IEnumerable<Player> GetEnemiesInRange(Player player) => GetInRangeLocations(player.Location)
            .Select(GetPlayerAtLocationOrDefault)
            .WhereNotNull()
            .Where(player.IsEnemy);

        public bool IsLocationOccupied(XY location)
        {
            return wallLocations.Contains(location) || playersByLocation.ContainsKey(location);
        }

        public Player GetPlayerAtLocationOrDefault(XY location)
        {
            playersByLocation.TryGetValue(location, out var player);
            return player;
        }

        public IReadOnlyCollection<Player> Players => playersByLocation.Values;

        public void MovePlayerFromTo(XY from, XY to)
        {
            if (IsLocationOccupied(to))
                throw new ArgumentException($"Destination location is already occupied {to}", nameof(to));

            RemovePlayerAtLocation(from, out var player);

            playersByLocation.Add(to, player);
        }

        public bool ContainsOnlyOneRacePlayers()
        {
            var firstPlayerType = Players.First().GetType();

            return Players.All(p => p.GetType() == firstPlayerType);
        }

        public string GenerateMap()
        {
            var topY = wallLocations.MaxBy(d => d.Y).Y;
            var rightX = wallLocations.MaxBy(d => d.X).X;

            var builder = new StringBuilder();

            for (var y = topY; y >= 0; y--)
            {
                var playersToMapOnSide = new List<Player>();

                for (var x = 0; x <= rightX; x++)
                {
                    var xy = new XY(x, y);
                    if (wallLocations.Contains(xy))
                    {
                        builder.Append('#');
                    }
                    else if (playersByLocation.TryGetValue(xy, out var player))
                    {
                        playersToMapOnSide.Add(player);
                        builder.Append(player.Symbol);
                    }
                    else
                    {
                        builder.Append('.');
                    }
                }

                builder.Append(playersToMapOnSide.Aggregate(string.Empty, (t, p) => $"{t}    {p.Symbol}({p.HitPoints})"));

                builder.AppendLine();
            }

            return builder.ToString();
        }

        private void RemovePlayerAtLocation(XY location, out Player player)
        {
            var wasRemoved = playersByLocation.Remove(location, out player);

            if (!wasRemoved)
                throw new ArgumentException($"Player does not exist at location {location}", nameof(location));
        }

        private IEnumerable<XY> EnumerateInRangeLocations(XY location)
        {
            yield return location + XY.Up;
            yield return location + XY.Left;
            yield return location + XY.Right;
            yield return location + XY.Down;
        }

        private void OnPlayerDied(Player player)
        {
            player.PlayerDied -= OnPlayerDied;
            player.PlayerMoved -= OnPlayerMoved;

            RemovePlayerAtLocation(player.Location, out var _); 
        }

        private void OnPlayerMoved(XY movedFrom, Player player)
        {
            MovePlayerFromTo(movedFrom, player.Location);
        }
    }
}
