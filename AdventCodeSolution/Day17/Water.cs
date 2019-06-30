using AdventCodeSolution.Day3;
using System.Collections.Generic;

namespace AdventCodeSolution.Day17
{
    public class Water
    {
        private readonly List<Water> furtherFlows;

        private Water(Water source, XY location)
        {
            Source = source;
            Location = location;

            furtherFlows = new List<Water>();
        }

        public XY Location { get; }

        public Water Source { get; }

        public bool IsStable { get; private set; }

        public IReadOnlyList<Water> FurtherFlows => furtherFlows;

        public void Stabilize()
        {
            IsStable = true;
        }

        public Water FlowToDirection(XY direction)
        {
            var water = new Water(this, Location + direction);

            furtherFlows.Add(water);

            return water;
        }
        
        public Water FindHigherLevelSource()
        {
            if (Source == null)
                return null;

            var currentSource = Source;

            while(Location.Y == currentSource.Location.Y)
            {
                currentSource = currentSource.Source;
            }

            return currentSource;
        }

        public static Water CreateSourceAt(XY location) => new Water(null, location);
    }
}
