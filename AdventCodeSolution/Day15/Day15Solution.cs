using AdventCodeSolution.Day15.Players;
using AdventCodeSolution.Day15.Players.Creation;
using System;
using System.Linq;

namespace AdventCodeSolution.Day15
{
    public class Day15Solution
    {
        public static void SolvePartOne()
        {
            var mapParser = new MapParser(new PlayersFactory());
            var map = mapParser.Parse(GetInput());

            var game = new Battle(map);

            game.Simulate();

            var hitPointsSum = game.Players.Sum(p => p.HitPoints);
            var result = game.TotalFullMovesMade * hitPointsSum;

            result.WriteLine("Day 15, Part 1: ");
        }

        public static void SolvePartTwo()
        {
            var game = FindElfsFirstWinByIncreasingTheirPower();
            //var game = RunGame(PlayerConfiguration.CreateConfig(14)).game;

            var hitPointsSum = game.Players.Sum(p => p.HitPoints);
            var result = game.TotalFullMovesMade * hitPointsSum;

            result.WriteLine("Day 15, Part 2: ");
        }

        private static Battle FindElfsFirstWinByIncreasingTheirPower()
        {
            var currentConfig = PlayerConfiguration.DefaultConfig;
            (Battle game, ElfsPerformanceResult elfResult) lastGameResult;

            do
            {
                currentConfig = PlayerConfiguration.CreateConfig(currentConfig.AttackPower + 1);
                lastGameResult = RunGame(currentConfig);

            } while (lastGameResult.elfResult.TotalElfDeaths > 0);

            return lastGameResult.game;
        }

        private static (Battle game, ElfsPerformanceResult elfResult) RunGame(PlayerConfiguration elfConfig)
        {
            var mapParser = new MapParser(new PlayersFactory(elfConfig));
            var map = mapParser.Parse(GetInput());

            var elfs = map.GetPlayerOfRace<Elf>();

            var game = new Battle(map);

            game.Simulate();

            var elfResult = new ElfsPerformanceResult(
                elfs.Length,
                elfs.Where(e => e.IsDead).Count());

            return (game, elfResult);
        }

        private static string GetInput() => InputResources.Day15Input;
    }
}
