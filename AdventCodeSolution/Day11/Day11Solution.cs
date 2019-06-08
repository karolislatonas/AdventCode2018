using System;
using System.Diagnostics;

namespace AdventCodeSolution.Day11
{
    public class Day11Solution
    {
        public static void SolvePartOne()
        {
            var gridSerialNumber = GetInput();

            var fuelGrid = new FuelGrid(gridSerialNumber, 300);

            var (square, powerLevel) = fuelGrid.GetSquareWithLargestPower(3);

            square.Position.WriteLine("Day 11, Part One: ");
        }

        public static void SolvePartTwo()
        {
            var gridSerialNumber = GetInput();

            var fuelGrid = new FuelGrid(gridSerialNumber, 300);

            var stopwatch = Stopwatch.StartNew();

            var squarePower = fuelGrid.GetSquareWithLargestPower();

            squarePower.square.WriteLine($"Day 11, Part One: ");
            stopwatch.ElapsedMilliseconds.WriteLine();
        }

        private static int GetInput() => int.Parse(InputResources.Day11Input);
    }
}
