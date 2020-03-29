namespace AdventCodeSolution.Day24
{
    public class Day24Solution
    {
        public static void SolvePartOne()
        {
            var immuneSystem = ImmuneSystemParser.Parse(GetInput());
            var immuneSystemSimulator = new ImmuneSystemSimulator();

            immuneSystemSimulator.Simulate(immuneSystem);

            var totalUnitsRemaining = immuneSystem.GetTotalUnitsRemaining();

            totalUnitsRemaining.WriteLine("Day 24, Part 1: ");
        }

        public static void SolvePartTwo()
        {
            var immuneSystemSimulator = new ImmuneSystemSimulator();

            var immuneSystem = immuneSystemSimulator.FindMinumumBoostForImmunitiesToSurvive(() => ImmuneSystemParser.Parse(GetInput()));

            var totalUnitsRemaining = immuneSystem.GetTotalUnitsRemaining();

            totalUnitsRemaining.WriteLine("Day 24, Part 2: ");
        }

        public static string GetInput() => InputResources.Day24Input;
    }
}
