namespace AdventCodeSolution.Day18
{
    public class Day18Solution
    {
        public static void SolvePartOne()
        {
            var magicArea = MagicAreaParser.Parse(GetInput());

            var magicAreaAfter10Minutes = magicArea.GetMagicAreaAfterMinutes(10);

            var result = magicAreaAfter10Minutes.LumberyardsCount * magicAreaAfter10Minutes.TreesCount;

            result.WriteLine("Day 18, Part 1: ");
        }

        public static void SolvePartTwo()
        {
            var magicArea = MagicAreaParser.Parse(GetInput());

            var magicAreaAfter10Minutes = magicArea.GetMagicAreaAfterMinutes(1000000000);

            var result = magicAreaAfter10Minutes.LumberyardsCount * magicAreaAfter10Minutes.TreesCount;

            result.WriteLine("Day 18, Part 1: ");
        }

        private static string GetInput() => InputResources.Day18Input;
    }
}
