using System;
using AdventCodeSolution.Day03;

namespace AdventCodeSolution.Day13
{
    public class Day13Solution
    {
        public static void SolvePartOne()
        {
            var (tracks, carts) = TracksCartsParser.Parse(GetInput());

            var trackSystem = new TrackSystem(tracks, carts);

            var firstCollison = trackSystem.FindFirstCollision();
            var systemHeight = trackSystem.GetSystemHeight();

            new XY(firstCollison.Location.X, systemHeight - firstCollison.Location.Y).WriteLine("Day 13, Part 1: ");
        }

        public static void SolvePartTwo()
        {
            var (tracks, carts) = TracksCartsParser.Parse(GetInput());

            var trackSystem = new TrackSystem(tracks, carts);

            var lastCart = trackSystem.FindLastCart();
            var systemHeight = trackSystem.GetSystemHeight();

            new XY(lastCart.Location.X, systemHeight - lastCart.Location.Y).WriteLine("Day 13, Part 2: ");
        }

        private static string GetInput() => InputResources.Day13Input;

        
    }
}
