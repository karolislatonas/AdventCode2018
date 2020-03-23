using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day23
{
    public class Day23Solution
    {
        public static void SolvePartOne()
        {
            var nanobots = InputParser.Parse(GetInput());

            var strongestRobot = nanobots.MaxBy(r => r.SignalRadius);
            var nanobotsInRange = nanobots.Count(r => strongestRobot.IsNanobotInRange(r));

            nanobotsInRange.WriteLine("Day 22, Part 1: ");
        }

        public static void SolvePartTwo()
        {
            var nanobots = InputParser.Parse(GetInput());

            var intersections = CalculateIntersectingPoints(nanobots);

            var shortestDistance = intersections
                .MultipleMaxBy(kvp => kvp.Value)
                .Select(kvp => kvp.Key.GetManhattanDistance(XYZ.Zero))
                .Min();

            shortestDistance.WriteLine("Day 22, Part 2: ");
        }

        private static string GetInput() => InputResources.Day23Input;

        private static Dictionary<XYZ, int> CalculateIntersectingPoints(IEnumerable<Nanobot> nanoRobots)
        {
            var intersections = nanoRobots.ToDictionary(r => r, r => FindIntersectingRobots(r, nanoRobots));

            var findMostIntersections = intersections
                .ToDictionary(kvp => kvp.Key, kvp => FindBiggestIntersection(kvp.Key, intersections))
                .MultipleMaxBy(kvp => kvp.Value.Count)
                .Select(kvp => kvp.Value)
                .ToArray();

            return null;
        }

        private static HashSet<Nanobot> FindBiggestIntersection(Nanobot nanoRobot, Dictionary<Nanobot, HashSet<Nanobot>> nanoRobots)
        {
            var intersectingRobots = nanoRobots[nanoRobot];

            var max = intersectingRobots
                .Where(r => nanoRobot != r)
                .Select(r =>
                {
                    var ppp = FindIntersectingRobots(r, intersectingRobots).ToHashSet();
                    return ppp;
                })
                .MaxBy(r => r.Count);

            return max;
        }

        private static HashSet<Nanobot> FindIntersectingRobots(Nanobot r, IEnumerable<Nanobot> nanoRobots)
        {
            return nanoRobots
                .Where(ro => ro.IsNanobotIntersecting(r))
                .ToHashSet();
        }
        
    }
}
