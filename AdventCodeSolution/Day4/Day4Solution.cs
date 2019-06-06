using AdventCodeSolution.Day4.GuardActions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day4
{
    public class Day4Solution
    {
        public static void SolvePartOne()
        {
            var guardActions = GetInput();

            var aggregator = new GuardActionAggregator();
            var guardPeriods = aggregator.AggregateGuardActions(guardActions);

            var mostSleepingGuardId = guardPeriods.FindMostSleepingGuard();
            var mostSleepingMinute = guardPeriods.FindMostSleepingMinuteOfGuard(mostSleepingGuardId);

            var result = mostSleepingGuardId * mostSleepingMinute;

            result.WriteLine("Day 4, Part 1: ");
        }

        public static void SolvePartTwo()
        {
            var guardActions = GetInput();

            var aggregator = new GuardActionAggregator();
            var guardPeriods = aggregator.AggregateGuardActions(guardActions);

            var mostSleepingGuardInAMinute = guardPeriods.FindMostSleepingGuardInAMinute();

            var result = mostSleepingGuardInAMinute.gaurdId * mostSleepingGuardInAMinute.mostSleepingMinute;

            result.WriteLine("Day 4, Part 2: ");
        }


        public static GuardAction[] GetInput() => InputResources.Day4Input
            .Split(Environment.NewLine)
            .Select(GuardActionParser.ParseGuardAction)
            .OrderBy(a => a.HappenedAt)
            .ToArray();
    }
}
