using AdventCodeSolution.Day03;
using System;
using System.Linq;

namespace AdventCodeSolution.Day22
{
    public class Day22Solution
    {
        public static void SolvePartOne()
        {
            var (depth, targetLocation) = GetInput();

            var caveScanner = new CaveScanner(depth);

            var result = caveScanner.EvaluateRisk(targetLocation);

            result.WriteLine("Day 22, Part 1: ");
        }

        public static void SolvePartTwo()
        {
            var (depth, targetLocation) = GetInput();

            var caveScanner = new CaveScanner(depth);

            var result = caveScanner.FindQuickestPathToTarget(targetLocation);

            result.Cost.WriteLine("Day 22, Part 2: ");
        }

        public static (int depth, XY targetLocation) GetInput()
        {
            var input = InputResources.Day22Input.Split(Environment.NewLine);

            var depthInput = input[0].Split(':').Last().Trim();
            var targetLocationInput = input[1].Split(':').Last().Trim();

            var depth = int.Parse(depthInput);
            var targetLocation = XY.Parse(targetLocationInput, ',');

            return (depth, targetLocation);
        }
    }
}
