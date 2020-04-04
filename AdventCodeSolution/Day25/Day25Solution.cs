using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day25
{
    public class Day25Solution
    {
        public static void SolvePartOne()
        {
            var points = GetInput();

            var constellations = points.Select(p => new Constellation(p)).ToHashSet();

            var anyWasMerged = false;
            do
            {
                anyWasMerged = false;
                var mergedCansellations = new HashSet<Constellation>();

                foreach (var constellation in constellations.Where(c => !mergedCansellations.Contains(c)))
                {
                    var constellationsToMerge = constellations
                        .Where(c => c != constellation)
                        .Where(c => !mergedCansellations.Contains(c))
                        .Where(constellation.CanBeMerged)
                        .ToArray();

                    anyWasMerged = anyWasMerged || constellationsToMerge.Length > 0;

                    mergedCansellations.AddRange(constellationsToMerge);

                    constellation.MergeAll(constellationsToMerge);
                }

                foreach (var constellation in mergedCansellations)
                    constellations.Remove(constellation);

            } while (anyWasMerged);

            var result = constellations.Count;

            result.WriteLine("Day 25, Part 1: ");
        }

        public static NPoint[] GetInput() => PointsParser.Parse(InputResources.Day25Input);
    }
}
