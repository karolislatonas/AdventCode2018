using AdventCodeSolution.Day03;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day22
{
    public class CaveScanner
    {
        private readonly int depth;

        public CaveScanner(int depth)
        {
            this.depth = depth;
        }

        public int EvaluateRisk(XY targetLocation)
        {
            var cave = new Cave(depth, targetLocation);

            var locations = EnumerateRegionLocationsWithoutTarget(cave.InitialLocation, cave.TargetLocation);

            return locations.Select(l => cave[l]).Sum(r => r.RiskLevel);
        }

        public CaveSearch FindQuickestPathToTarget(XY targetLocation)
        {
            var cave = new Cave(depth, targetLocation);
            var pathFinder = new PathFinder();

            return pathFinder.FindQuickestPath(cave);
        }

        private IEnumerable<XY> EnumerateRegionLocationsWithoutTarget(XY initialLocation, XY target)
        {
            var targetX = target.X + 1;

            return initialLocation.StartEnumerate(l =>
            {
                var possibleNextX = l.X + 1;
                var nextX = possibleNextX % targetX;
                var nextY = l.Y + (possibleNextX / targetX);

                return new XY(nextX, nextY);

            }).TakeWhile(l => l != target);
        }


    }
}
