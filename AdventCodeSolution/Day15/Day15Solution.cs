using AdventCodeSolution.Day15.Players;
using AdventCodeSolution.Day15.Players.Creation;
using System.Linq;

namespace AdventCodeSolution.Day15
{
    public class Day15Solution
    {
        public static void SolvePartOne()
        {
            var mapParser = new BattleMapParser(new PlayersFactory());
            var map = mapParser.Parse(GetInput());

            var battle = new Battle(map);

            battle.Simulate();

            var hitPointsSum = battle.Players.Sum(p => p.HitPoints);
            var result = battle.TotalFullMovesMade * hitPointsSum;

            result.WriteLine("Day 15, Part 1: ");
        }

        public static void SolvePartTwo()
        {
            var battle = FindElfsFirstWinByIncreasingTheirPower();

            var hitPointsSum = battle.Players.Sum(p => p.HitPoints);
            var result = battle.TotalFullMovesMade * hitPointsSum;

            result.WriteLine("Day 15, Part 2: ");
        }

        private static Battle FindElfsFirstWinByIncreasingTheirPower()
        {
            // Todo think of more performant solution
            var battleWithoutLosses = PlayerConfiguration.DefaultConfig
                .StartEnumerate(c => PlayerConfiguration.CreateConfig(c.AttackPower + 1))
                .Select(SimulateBattle)
                .First(b => b.elfResult.TotalElfDeaths == 0);

            return battleWithoutLosses.battle;
        }

        private static (Battle battle, ElfsPerformanceResult elfResult) SimulateBattle(PlayerConfiguration elfConfig)
        {
            var mapParser = new BattleMapParser(new PlayersFactory(elfConfig));
            var map = mapParser.Parse(GetInput());

            var elfs = map.GetPlayersOfRace<Elf>();

            var battle = new Battle(map);

            battle.Simulate();

            var elfsResult = new ElfsPerformanceResult(
                elfs.Length,
                elfs.Where(e => e.IsDead).Count());

            return (battle, elfsResult);
        }

        private static string GetInput() => InputResources.Day15Input;
    }
}
