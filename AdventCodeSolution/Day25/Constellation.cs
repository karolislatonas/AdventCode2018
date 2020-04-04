using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day25
{
    public class Constellation
    {
        private const int maxDistance = 3;
        private readonly HashSet<NPoint> points;

        public Constellation() :
            this(Enumerable.Empty<NPoint>())
        {

        }

        public Constellation(NPoint point) :
            this(point.RepeatOnce())
        {
            
        }

        public Constellation(IEnumerable<NPoint> points)
        {
            this.points = points.ToHashSet();
        }

        public void Add(NPoint point)
        {
            if (!IsPointCloseEnough(point))
                throw new ArgumentException("Point is too far away");

            points.Add(point);
        }

        public bool IsPointCloseEnough(NPoint point)
        {
            return points.Count == 0 ||
                   points.Any(p => p.GetManhattanDistance(point) <= maxDistance);
        }

        public void Merge(Constellation other)
        {
            if(!CanBeMerged(other))
                throw new ArgumentException("None of a points are close enough");

            points.AddRange(other.points);
        }

        public void MergeAll(IEnumerable<Constellation> constellations)
        {
            foreach (var constellation in constellations)
                Merge(constellation);
        }

        public bool CanBeMerged(Constellation other)
        {
            return other.points.Any(IsPointCloseEnough);
        }
    }
}
