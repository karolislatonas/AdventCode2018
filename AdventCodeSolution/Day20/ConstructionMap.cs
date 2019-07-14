using AdventCodeSolution.Day3;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AdventCodeSolution.Day20
{
    public class ConstructionMap
    {
        private static readonly XY[] PossibleDirections = { XY.Up, XY.Right, XY.Down, XY.Left };

        private readonly HashSet<XY> doorsAndRoomLocations;

        public ConstructionMap(Path path)
        {
            doorsAndRoomLocations = path.EnumerateLocationsInludingForkedPaths().ToHashSet();
        }

        public int CountDoorsToFurthestRoom()
        {
            var longestPath = EnumeratePossiblePaths().Last();

            return longestPath.Count - 1;
        }

        public int CountRoomsThatNeedAtLeastNumberOfDoorsToPass(int numberOfDoorsToPass)
        {
            return EnumeratePossiblePaths()
                .Where(p => p.Count - 1 >= numberOfDoorsToPass)
                .Count();
        }

        public IEnumerable<IList<XY>> EnumeratePossiblePaths()
        {
            var startingPath = ImmutableList.Create(XY.Zero);
            var pathsToFork = new List<ImmutableList<XY>>() { startingPath };
            var visitedLocations = startingPath.ToHashSet();

            while (pathsToFork.Count > 0)
            {
                var forkedPaths = new List<ImmutableList<XY>>();

                foreach(var pathToFork in pathsToFork)
                {
                    var pathsWhichNotReachedBefore = ForkPathToPossibleWays(pathToFork)
                        .Where(p => !visitedLocations.Contains(p.Last()))
                        .ToArray();

                    var endsOfNewPaths = pathsWhichNotReachedBefore.Select(p => p.Last());

                    forkedPaths.AddRange(pathsWhichNotReachedBefore);
                    visitedLocations.AddRange(endsOfNewPaths);
                }

                foreach (var path in forkedPaths)
                    yield return path;

                pathsToFork = forkedPaths;
            }
        }

        private IEnumerable<ImmutableList<XY>> ForkPathToPossibleWays(ImmutableList<XY> path)
        {
            var end = path.Last();

            var forkToLocations = PossibleDirections
                .Where(d => doorsAndRoomLocations.Contains(end + d))
                .Select(d => end + d + d)
                .ToArray();

            var forkedPaths = forkToLocations.Select(path.Add);

            return forkedPaths;
        }
    }
}
