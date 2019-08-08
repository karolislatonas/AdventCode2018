using AdventCodeSolution.Day03;
using AdventCodeSolution.Day22.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day22
{
    public class CaveScanner
    {
        private static CaveRegionFactory caveRegionFactory = new CaveRegionFactory();
        private readonly XY initialLocation = XY.Zero;
        private readonly int depth;
        

        public CaveScanner(int depth)
        {
            this.depth = depth;
        }

        public int EvaluateRisk(XY targetLocation)
        {
            var scannedRegions = ScanCave(targetLocation);

            return scannedRegions.Values.Sum(r => r.RiskLevel);
        }

        private IReadOnlyDictionary<XY, CaveRegion> ScanCave(XY target)
        {
            var locationsTillTarget = EnumerateRegionLocationsWithoutTarget(target);

            var foundRegions = new Dictionary<XY, CaveRegion>();

            foreach (var location in locationsTillTarget)
            {
                var region = DetermineCaveRegion(location, l => foundRegions[l]);
                foundRegions.Add(location, region);
            }

            return foundRegions;
        }

        private CaveRegion DetermineCaveRegion(XY location, Func<XY, CaveRegion> getRegion)
        {
            var geologicalIndex = GetGeologicIndex(location, getRegion);
            var eriosionLevel = CalculateErosionLevel(geologicalIndex);

            return caveRegionFactory.Create(eriosionLevel);
        }

        private int GetGeologicIndex(XY location, Func<XY, CaveRegion> getRegion)
        {
            if (location == XY.Zero)
                return 0;

            if (location.Y == 0)
                return location.X * 16807;

            if (location.X == 0)
                return location.Y * 48271;

            var leftCaveRegion = getRegion(location + XY.Left);
            var bottomCaveRegion = getRegion(location + XY.Down);

            return leftCaveRegion.ErosionLevel * bottomCaveRegion.ErosionLevel;
        }

        private int CalculateErosionLevel(int geologicIndex) => (depth + geologicIndex) % 20183;

        private IEnumerable<XY> EnumerateRegionLocationsWithoutTarget(XY target)
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
