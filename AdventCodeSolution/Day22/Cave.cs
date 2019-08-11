using AdventCodeSolution.Day03;
using AdventCodeSolution.Day22.Regions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AdventCodeSolution.Day22
{
    public class Cave
    {
        private static readonly CaveRegionFactory caveRegionFactory = new CaveRegionFactory();
        private readonly IDictionary<XY, CaveRegion> foundRegions = new ConcurrentDictionary<XY, CaveRegion>();
        private readonly int depth;

        public Cave(int depth, XY targetLocation)
        {
            this.depth = depth;
            TargetLocation = targetLocation;
        }

        public XY InitialLocation => XY.Zero;

        public XY TargetLocation { get; }

        public CaveRegion this[XY location] => GetRegionAtLocation(location);

        private CaveRegion GetRegionAtLocation(XY location)
        {
            if(!foundRegions.TryGetValue(location, out var region))
            {
                region = DetermineCaveRegion(location, l => GetRegionAtLocation(l));
                foundRegions[location] = region;
            }

            return region;
        }

        private CaveRegion DetermineCaveRegion(XY location, Func<XY, CaveRegion> getRegion)
        {
            var geologicalIndex = GetGeologicIndex(location, getRegion);
            var eriosionLevel = CalculateErosionLevel(geologicalIndex);

            return location == TargetLocation ?
                new RockyRegion(eriosionLevel) :
                caveRegionFactory.Create(eriosionLevel);
        }

        private int GetGeologicIndex(XY location, Func<XY, CaveRegion> getRegion)
        {
            if (location == InitialLocation)
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
    }
}
