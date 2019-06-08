using AdventCodeSolution.Day3;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day11
{
    public class FuelGrid
    {
        private static readonly XY GridStart = new XY(1, 1);
        private readonly int gridSize;
        private readonly int gridSerialNumber;

        public FuelGrid(int gridSerialNumber, int gridSize)
        {
            this.gridSize = gridSize;
            this.gridSerialNumber = gridSerialNumber;
        }

        public (Square square, int powerLevel) GetSquareWithLargestPower(int squareSize)
        {
            return EnumerateSquarePowersOfSize(squareSize).MaxBy(s => s.powerLevel);
        }

        public (Square square, int powerLevel) GetSquareWithLargestPower()
        {
            return Enumerable.Range(1, gridSize)
                .AsParallel()
                .Select(s => GetSquareWithLargestPower(s))
                .MaxBy(s => s.powerLevel);
        }

        private IEnumerable<(Square square, int powerLevel)> EnumerateSquarePowersOfSize(int squareSize)
        {
            var maxSize = gridSize - squareSize + 1;
            var firstSquare = new Square(GridStart, squareSize);

            var currentSquarePower = (square: firstSquare, power: GetSquarePower(firstSquare));
            var previousLeftSideSquarePower = currentSquarePower;

            yield return currentSquarePower;

            for (var y = 0; y < maxSize; y++)
            {
                for (var x = 1; x < maxSize; x++)
                {
                    currentSquarePower = CalculateNextSquarePower(GridStart.X + x, GridStart.Y + y, currentSquarePower, CalculateNextRightSquarePower);
                    yield return currentSquarePower;
                }

                currentSquarePower = CalculateNextSquarePower(GridStart.X, GridStart.Y + y + 1, previousLeftSideSquarePower, CalculateNextUpperSquarePower);
                previousLeftSideSquarePower = currentSquarePower;

                yield return currentSquarePower;
            }
        }

        private (Square square, int powerLevel) CalculateNextSquarePower(
            int nextSquareX, int nextSquareY, (Square square, int power) squarePower, Func<Square, int, int> calculateNextSquarePower)
        {
            var nextSquare = new Square(new XY(nextSquareX, nextSquareY), squarePower.square.Size);
            var nextSquarePower = calculateNextSquarePower(squarePower.square, squarePower.power);

            return (nextSquare, nextSquarePower);
        }

        private int CalculateNextRightSquarePower(Square square, int squarePower)
        {
            var startRightFuelsToAdd = new XY(square.Position.X + square.Size, square.Position.Y);

            var leftPowersToRemove = EnumerateRectanglePositions(square.Position, 1, square.Size);
            var rightPowersToAdd = EnumerateRectanglePositions(startRightFuelsToAdd, 1, square.Size);

            return squarePower
                - SumPower(leftPowersToRemove)
                + SumPower(rightPowersToAdd);
        }

        private int CalculateNextUpperSquarePower(Square square, int squarePower)
        {
            var startOfUpperFuelsToAdd = new XY(square.Position.X, square.Position.Y + square.Size);

            var bottomPowersToRemove = EnumerateRectanglePositions(square.Position, square.Size, 1);
            var topPowersToAdd = EnumerateRectanglePositions(startOfUpperFuelsToAdd, square.Size, 1);

            return squarePower
                - SumPower(bottomPowersToRemove)
                + SumPower(topPowersToAdd);
        }

        private int GetSquarePower(Square square) => EnumerateSquarePositions(square).Sum(xy => CalculatePower(xy, gridSerialNumber));

        private int SumPower(IEnumerable<XY> xys) => xys.Sum(xy => CalculatePower(xy, gridSerialNumber));

        private static IEnumerable<XY> EnumerateSquarePositions(Square square) => EnumerateRectanglePositions(square.Position, square.Size, square.Size);

        private static IEnumerable<XY> EnumerateRectanglePositions(XY start, int xSize, int ySize)
        {
            for (var x = 0; x < xSize; x++)
                for (var y = 0; y < ySize; y++)
                    yield return new XY(start.X + x, start.Y + y);
        }

        private static int CalculatePower(XY position, int gridSerialNumber)
        {
            var rackId = position.X + 10;

            var powerLevel = rackId * position.Y;
            powerLevel += gridSerialNumber;
            powerLevel *= rackId;
            powerLevel = (powerLevel / 100) % 10;
            powerLevel -= 5;

            return powerLevel;
        }
    }
}
