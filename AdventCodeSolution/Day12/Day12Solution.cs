using System;
using System.Collections.Generic;
using System.Linq;
using static AdventCodeSolution.Day12.PlantPot;

namespace AdventCodeSolution.Day12
{
    public class Day12Solution
    {
        public static void SolvePartOne()
        {
            var initialState = GetInitialState().Select((p, i) => new PlantPot(i, ParseContainsPlant(p)));
            var rules = GetRules();

            var plantGenerationCalculator = new PlantsGenerationCalculator(initialState, rules);

            var plantsOfFutureGeneration = plantGenerationCalculator.GetPlantsOfGeneration(20);
            
            var sumOfPotNumbersWithPlants = plantsOfFutureGeneration.Where(p => p.ContainsPlant).Sum(p => p.Number);

            sumOfPotNumbersWithPlants.WriteLine("Day 12, Part 1: ");
        }

        public static void SolvePartTwo()
        {
            var initialState = GetInitialState().Select((p, i) => new PlantPot(i, ParseContainsPlant(p)));
            var rules = GetRules();

            var plantGenerationCalculator = new PlantsGenerationCalculator(initialState, rules);

            var plantsOfFutureGeneration = plantGenerationCalculator.GetPlantsOfGeneration(50000000000);

            var sumOfPotNumbersWithPlants = plantsOfFutureGeneration.Where(p => p.ContainsPlant).Sum(p => p.Number);

            sumOfPotNumbersWithPlants.WriteLine("Day 12, Part 2: ");
        }

        private static string GetInitialState() => InputResources.Day12Input
            .Split(Environment.NewLine)
            .First()
            .TrimStart("initial state: ");

        private static Dictionary<string, bool> GetRules() => InputResources.Day12Input
            .Split(Environment.NewLine)
            .Skip(2)
            .Select(i => i.Split('=', '>'))
            .Select(i => (pattern: i.First().Trim(), result: ParseContainsPlant(i.Last().Trim().Single())))
            .ToDictionary(i => i.pattern, i => i.result);

    }
}
