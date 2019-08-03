using AdventCodeSolution.Day03;
using System;
using System.Linq;

namespace AdventCodeSolution.Day10
{
    public class Day10Solution
    {
        public static void SolvePartOne()
        {
            var lights = GetInput();

            var enumerationFromZero = 0.EnumerateFrom();

            var lightsPositions = enumerationFromZero
                .Select(s => lights.Select(l => l.GetPositionAfterSeconds(s)).ToArray())
                .First(ls => GetMaxYDistanance(ls) <= 9)
                .Distinct()
                .OrderBy(v => v, FromLeftHighestToRightLowestComparer.Value)
                .ToArray();

            Console.Write("Day 10, Part One");
            Console.WriteLine();
            OutputPositions(lightsPositions);
        }

        public static void SolvePartTwo()
        {
            var lights = GetInput();

            var enumerationFromZero = 0.EnumerateFrom();

            var secondAfterWordAppeared = enumerationFromZero
                .Select(s => (second: s, lights: lights.Select(l => l.GetPositionAfterSeconds(s)).ToArray()))
                .First(ls => GetMaxYDistanance(ls.lights) <= 9)
                .second;

            secondAfterWordAppeared.WriteLine("Day 10, Part Two: ");
        }

        private static int GetMaxYDistanance(XY[] lights)
        {
            var minY = lights.Select(l => l.Y).Min();
            var maxY = lights.Select(l => l.Y).Max();

            return maxY - minY;
        }

        private static void OutputPositions(XY[] positions)
        {
            var mostLeftX = positions.Select(x => x.X).Min();

            void WriteOutput(int skip)
            {
                Console.Write(Enumerable.Repeat('.', skip).ToArray());
                Console.Write('#');
            }

            WriteOutput(positions[0].X - mostLeftX);

            for (var i = 1; i < positions.Length; i++)
            {
                var current = positions[i];
                var previous = positions[i - 1];

                var isInNewLine = current.Y - previous.Y != 0;
                if (isInNewLine)
                {
                    Console.WriteLine();
                    WriteOutput(current.X - mostLeftX);
                }
                else
                {
                    WriteOutput(current.X - previous.X - 1);
                }
            }
        }

        private static Light[] GetInput() => InputResources.Day10Input.Split(Environment.NewLine)
            .Select(inputLine => inputLine.TrimStart("position=").Split(" velocity="))
            .Select(i => (positionInput: i.First().Trim('<', '>'), velocityInput: i.Last().Trim('<', '>')))
            .Select(i => new Light(XY.Parse(i.positionInput, ','), XY.Parse(i.velocityInput, ',')))
            .ToArray();
    }
}
