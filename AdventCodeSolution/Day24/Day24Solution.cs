namespace AdventCodeSolution.Day24
{
    public class Day24Solution
    {
        public static void SolvePartOne()
        {
            var immuneSystem = ImmuneSystemParser.Parse(GetInput());
            var targetSelect = new TargetSelector(immuneSystem);
            var attackExecuter = new AttackExecuter();

            do
            {
                var unitAttacks = targetSelect.SelectTargets();
                attackExecuter.ExecuteAttacks(unitAttacks);

            } while (immuneSystem.BothGroupsRemaining());

            var totalUnitsRemaining = immuneSystem.GetTotalUnitsRemaining();

            totalUnitsRemaining.WriteLine("Day 24, Part 1: ");
        }

        public static void SolvePartTwo()
        {
            var immuneSystem = ImmuneSystemParser.Parse(GetInput());
            var targetSelect = new TargetSelector(immuneSystem);
            var attackExecuter = new AttackExecuter();

            do
            {
                var unitAttacks = targetSelect.SelectTargets();
                attackExecuter.ExecuteAttacks(unitAttacks);

            } while (immuneSystem.BothGroupsRemaining());

            var totalUnitsRemaining = immuneSystem.GetTotalUnitsRemaining();

            totalUnitsRemaining.WriteLine("Day 24, Part 1: ");
        }

        public static string GetInput() => InputResources.Day24Input;
    }
}
