using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day5
{
    public class Day5Solution
    {
        public static void SolvePartOne()
        {
            var polymer = new Polymer();

            foreach(var unit in GetInput())
            {
                polymer.AddUnit(unit);
            }

            polymer.Length.WriteLine("Day 5, Part 1: ");
        }

        public static void SolvePartTwo()
        {
            var input = GetInput();

            var polymerUnitsToFilter = new HashSet<char>(input.Select(char.ToLower));

            var polymersWithFilters = polymerUnitsToFilter
                .Select(f => new UnitFilter(f))
                .Select(f => new Polymer(f))
                .ToArray();

            foreach (var polymer in polymersWithFilters)
                foreach (var unit in input)
                {
                    polymer.AddUnit(unit);
                }

            var shortestPolymer = polymersWithFilters.MinBy(p => p.Length);

            shortestPolymer.Length.WriteLine("Day 5, Part 2: ");
        }

        public static string GetInput() => InputResources.Day5Input;
    }
}
