using AdventCodeSolution.Day03;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day20
{
    public class Path
    {
        private readonly List<XY> locations;
        private readonly List<Path> forks;

        public Path(XY start)
        {
            locations = new List<XY>() { start };
            forks = new List<Path>();
        }

        private Path(Path from, XY start) : this(start)
        {
            StartedFrom = from;
        }

        public IReadOnlyList<XY> PathLocations => locations;

        public IReadOnlyList<Path> ForkedPaths => forks;

        public Path StartedFrom { get; }

        public Path GetInitialPath()
        {
            var current = this;
            while(current.StartedFrom != null)
            {
                current = current.StartedFrom;
            }

            return current;
        }

        public void MoveInDirection(XY direction)
        {
            locations.Add(locations.Last() + direction); // Moving Through a door
            locations.Add(locations.Last() + direction); // Moving to a room
        }

        public Path ForkPathFromLastLocation()
        {
            var newFork = new Path(this, locations.Last());
            forks.Add(newFork);

            return newFork;
        }

        public IEnumerable<XY> EnumerateLocationsInludingForkedPaths()
        {
            var forkedPathLocations = ForkedPaths.SelectMany(f => f.EnumerateLocationsInludingForkedPaths());

            return PathLocations.Concat(forkedPathLocations);
        }
    }
}
