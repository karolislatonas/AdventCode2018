using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day3
{
    public class Day3Solution
    {
        public static void SolvePartOne()
        {
            var rectangles = GetInput();

            var overlaps = rectangles.SelectMany(r1 => rectangles
                .Where(r2 => r1.id != r2.id)
                .Select(r2 => r1.rectangle.GetOverlap(r2.rectangle))
                .WhereNotNull());

            var points = new HashSet<XY>(overlaps.SelectMany(r => r.EnumeratePoints()));

            points.Count().WriteLine("Day 3, Part 1: ");
        }

        public static void SolvePartTwo()
        {
            var rectangles = GetInput();

            var notOverlappingRectangle = rectangles.Single(r1 => rectangles
                .Where(r2 => r1.id != r2.id)
                .All(r2 => r1.rectangle.GetOverlap(r2.rectangle) == null));

            notOverlappingRectangle.id.WriteLine("Day 3, Part 2: ");
        }

        private static (int id, Rectangle rectangle) ParseRectangle(string toParse)
        {
            var values = toParse.Split('@', ':').Select(v => v.Trim()).ToArray();

            var id = int.Parse(values[0].Trim('#'));

            var locationToParse = values[1].Split(',');
            var sizeToParse = values[2].Split('x');

            var location = new XY(int.Parse(locationToParse.First()), int.Parse(locationToParse.Last()));
            var size = new XY(int.Parse(sizeToParse.First()), int.Parse(sizeToParse.Last()));



            return (id, new Rectangle(location, size));
        }

        private static (int id, Rectangle rectangle)[] GetInput() => InputResources.Day3Input.Split(Environment.NewLine).Select(ParseRectangle).ToArray();
    }
}
