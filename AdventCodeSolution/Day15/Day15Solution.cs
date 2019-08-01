using AdventCodeSolution.Day15.Players;
using AdventCodeSolution.Day15.Players.Creation;
using System;
using System.Diagnostics;
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
            var stopwatch = Stopwatch.StartNew();

            var defaultConfig = PlayerConfiguration.DefaultConfig;

            var attackPowerWithOutLosses = BinarySearch.ForawrdSearch(defaultConfig.AttackPower,
                attackPower => attackPower * attackPower,
                attackPower =>
                {
                    var battleResult = SimulateBattle(PlayerConfiguration.CreateConfig(attackPower));
                    return battleResult.elfResult.TotalElfDeaths == 0 ? -1 : 1;
                });

            return SimulateBattle(PlayerConfiguration.CreateConfig(attackPowerWithOutLosses)).battle;
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
