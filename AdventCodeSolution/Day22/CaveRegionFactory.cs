using AdventCodeSolution.Day22.Regions;
using System;
using System.Collections.Generic;

namespace AdventCodeSolution.Day22
{
    public class CaveRegionFactory
    {
        private static readonly IReadOnlyDictionary<int, Func<int, CaveRegion>> CaveRegionFactories =
            new Dictionary<int, Func<int, CaveRegion>>()
            {
                [0] = e => new RockyRegion(e),
                [1] = e => new WetRegion(e),
                [2] = e => new NarrowRegion(e),
            };

        public CaveRegion Create(int erosionLevel)
        {
            var regionNumber = erosionLevel % 3;

            return CaveRegionFactories[regionNumber](erosionLevel);
        }
    }
}
