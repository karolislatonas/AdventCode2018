using AdventCodeSolution.Day03;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day17
{
    public class ClayMap
    {
        private readonly HashSet<XY> clayLocations;
        private readonly Lazy<int> bottomBoundary;
        private readonly Lazy<int> topBoundary;

        public ClayMap(IEnumerable<XY> clayLocations)
        {
            this.clayLocations = clayLocations.ToHashSet();

            bottomBoundary = new Lazy<int>(() => this.clayLocations.MinBy(l => l.Y).Y);
            topBoundary = new Lazy<int>(() => this.clayLocations.MaxBy(l => l.Y).Y);
        }

        public IReadOnlyCollection<XY> ClayLocations => clayLocations;

        public bool IsBelowMap(XY location)
        {
            return location.Y < bottomBoundary.Value;
        }

        public bool IsInClayArea(XY location)
        {
            return bottomBoundary.Value <= location.Y && location.Y <= topBoundary.Value;
        }

        public bool IsClayAtLocation(XY location) => clayLocations.Contains(location);
    }
}
