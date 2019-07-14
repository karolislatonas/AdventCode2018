namespace AdventCodeSolution.Day20
{
    public class Day20Solution
    {
        public static void SolvePartOne()
        {
            var path = PathParser.Parse(GetInput());
            var map = new ConstructionMap(path);

            var result = map.CountDoorsToFurthestRoom();

            result.WriteLine("Day 20, Part 1: ");
        }

        public static void SolvePartTwo()
        {
            var path = PathParser.Parse(GetInput());
            var map = new ConstructionMap(path);

            var result = map.CountRoomsThatNeedAtLeastNumberOfDoorsToPass(1000);

            result.WriteLine("Day 20, Part 2: ");
        }

        private static string GetInput() => InputResources.Day20Input; 
    }
}
