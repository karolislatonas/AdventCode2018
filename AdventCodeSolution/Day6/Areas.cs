using AdventCodeSolution.Day3;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventCodeSolution.Day6
{
    public class Areas
    {
        private readonly IDictionary<XY, Area> locationAreas;

        public Areas(IEnumerable<XY> locations)
        {
            var locationArray = locations.ToArray();

            var mostLeft = locationArray.Select(c => c.X).Min();
            var mostRight = locationArray.Select(c => c.X).Max();
            var mostTop = locationArray.Select(c => c.Y).Min();
            var mostBottom = locationArray.Select(c => c.Y).Max();

            TopLeft = new XY(mostLeft, mostTop);
            BottomRight = new XY(mostRight, mostBottom);

            locationAreas = GetAreaOfLocations(locationArray, TopLeft, BottomRight);
        }

        public XY TopLeft { get; }

        public XY BottomRight { get; }

        public Area GetBiggestFiniteArea()
        {
            return locationAreas.Values
                .Where(a => a.IsFiniteOnArea(TopLeft, BottomRight))
                .MaxBy(a => a.Size);
        }

        public IEnumerable<XY> PointsWithManhattanDistanceLessThan(int limit)
        {
            return EnumerateAreaPoints(TopLeft, BottomRight)
                .Select(p => (point: p, manhattanDistanceSum: locationAreas.Keys.Select(l => l.GetManhattanDistance(p)).Sum()))
                .Where(p => p.manhattanDistanceSum < limit)
                .Select(p => p.point);
        }

        private IDictionary<XY, Area> GetAreaOfLocations(XY[] locations, XY topLeft, XY bottomRight)
        {
            var iniatalAreas = locations.Select(p => KeyValuePair.Create(p, new ConcurrentBag<XY>()));
            var areasAggregate = new ConcurrentDictionary<XY, ConcurrentBag<XY>>(iniatalAreas);

            EnumerateAreaPoints(topLeft, bottomRight)
                .AsParallel()
                .Aggregate(areasAggregate, (areas, p) =>
                {
                    var locationsWithClosestDistance = locations.MultipleMinBy(l => l.GetManhattanDistance(p));

                    if (locationsWithClosestDistance.Length > 1)
                        return areas;

                    var closestLocation = locationsWithClosestDistance.Single();
                    areas[closestLocation].Add(p);

                    return areas;
                });

            return areasAggregate.ToDictionary(kvp => kvp.Key, kvp => new Area(kvp.Key, kvp.Value));
        }
        

        private IEnumerable<XY> EnumerateAreaPoints(XY topLeft, XY bottomRight)
        {
            var totalX = bottomRight.X - topLeft.X + 1;
            var totalY = bottomRight.Y - topLeft.Y + 1;

            for (var x = 0; x < totalX; x++)
                for (var y = 0; y < totalY; y++)
                {
                    yield return new XY(TopLeft.X + x, TopLeft.Y + y);
                }
        }
    }
}
