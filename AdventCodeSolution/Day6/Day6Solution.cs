using AdventCodeSolution.Day3;
using System;
using System.Linq;

namespace AdventCodeSolution.Day6
{
    public class Day6Solution
    {
        public static void SolvePartOne()
        {
            var areas = new Areas(GetInput());

            areas.GetBiggestFiniteArea().Size.WriteLine("Day 6, Part 1: ");
        }

        public static void SolvePartTwo()
        {
            var areas = new Areas(GetInput());

            areas.PointsWithManhattanDistanceLessThan(10000)
                .Count()
                .WriteLine("Day 6, Part 2: ");
        }

        public static XY[] GetInput() => InputResources.Day6Input
            .Split(Environment.NewLine)
            .Select(xy => XY.Parse(xy, ','))
            .ToArray();
    }
}
