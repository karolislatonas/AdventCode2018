using AdventCodeSolution.Day3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventCodeSolution.Day17
{
    public class Day17Solution
    {
        public static void SolvePartOne()
        {
            var clayLocations = GetInput();

            var sourceLocation = new XY(500, 0);
            var map = new ClayMap(clayLocations);
            var stream = new WaterStreamBuilder(sourceLocation, map);

            var water = stream.GetStream();

            var result = water.Select(w => w.Location).Count(map.IsInClayArea);

            //File.WriteAllText(
            //    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "map.txt"), 
            //    CreateMap(sourceLocation, map, water).ToString());

            result.WriteLine("Day 17, Part 1: ");
        }

        public static void SolvePartTwo()
        {
            var clayLocations = GetInput();

            var sourceLocation = new XY(500, 0);
            var map = new ClayMap(clayLocations);
            var streamBuilder = new WaterStreamBuilder(sourceLocation, map);

            var stream = streamBuilder.GetStream();

            var result = stream.CountStableWater();

            result.WriteLine("Day 17, Part 2: ");
        }

        public static XY[] GetInput()
        {
            return InputResources.Day17Input
                .Split(Environment.NewLine)
                .SelectMany(ParseLocations)
                .ToArray();
        }

        private static IEnumerable<XY> ParseLocations(string input)
        {
            var xyInputs = input.Split(',').Select(i => i.Trim()).ToArray();

            var xValues = ParseRangeValues('x', xyInputs);
            var yValues = ParseRangeValues('y', xyInputs);

            return xValues.SelectMany(x => yValues.Select(y => new XY(x, -y))).ToArray();
        }

        private static IEnumerable<int> ParseRangeValues(char variableName, string[] inputs)
        {
            var rangeValues = inputs
                .Single(i => i.StartsWith(variableName))
                .TrimStart($"{variableName}=")
                .Split("..")
                .Select(int.Parse)
                .ToArray();

            return Enumerable.Range(rangeValues.First(), rangeValues.Last() - rangeValues.First() + 1);
        }

        private static StringBuilder CreateMap(XY sourceLocation, ClayMap clayMap, IDictionary<XY, Water> existingWater)
        {
            var topBoundary = 0;
            var bottomBoundary = clayMap.ClayLocations.MinBy(l => l.Y).Y - 1;
            var leftBoundary = clayMap.ClayLocations.MinBy(l => l.X).X - 5;
            var rightBoundary = clayMap.ClayLocations.MaxBy(l => l.X).X + 5;

            var yLocations = Enumerable.Range(bottomBoundary, topBoundary - bottomBoundary + 1).Reverse();
            var xLocations = Enumerable.Range(leftBoundary, rightBoundary - leftBoundary + 1);

            var builder = new StringBuilder();

            foreach (var y in yLocations)
            {
                foreach (var x in xLocations)
                {
                    var l = new XY(x, y);

                    if (clayMap.IsClayAtLocation(l)) builder.Append('#');
                    else if (existingWater.ContainsKey(l))
                    {
                        if(l == sourceLocation) builder.Append('+');
                        else if (existingWater[l].IsStable) builder.Append('~');
                        else builder.Append('|');
                    }   
                    else builder.Append(' ');
                }

                builder.AppendLine();
            }

            return builder;
        }
    }
}
