using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day01
{
    public class Day1Solution
    {
        public static void SolvePartOne()
        {
            GetInput().Sum().WriteLine("Day 1. Part 1: ");
        }

        public static void SolvePartTwo()
        {
            var start = 0;
            var occurencies = new HashSet<int>() { start };

            var frequencies = GetInput().ToArray();

            var current = start;

            foreach (var frequency in frequencies.Infinitely())
            {
                current += frequency;
                var wasAdded = occurencies.Add(current);
                if (!wasAdded)
                    break;
            }

            current.WriteLine("Day 1. Part 2: ");
        }

        private static IEnumerable<int> GetInput() => InputResources.Day1Input
                .Split(Environment.NewLine)
                .Select(n => int.Parse(n));


    }
}
