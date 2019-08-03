using AdventCodeSolution.Day04.GuardActions;
using AdventCodeSolution.Day04.Periods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day04
{
    public class GuardActionAggregator
    {
        public GuardsPeriods AggregateGuardActions(GuardAction[] actions)
        {
            var initialAction = actions.First();

            var aggregatedValue = new GuardsPeriods((BeginsShift)initialAction);

            foreach(var action in actions.Skip(1))
            {
                aggregatedValue.AddAction((dynamic)action);
            }

            return aggregatedValue;
        }
    }

    public class GuardsPeriods
    {
        private static readonly TimeSpan OneMinute = TimeSpan.FromMinutes(1);

        private readonly List<GuardTimePeriod> periods = new List<GuardTimePeriod>();

        private Func<DateTime, GuardTimePeriod> currentActionFactory;

        public GuardsPeriods(BeginsShift initialAction)
        {
            SetAwakePeriodFacotry(initialAction);
        }

        public int FindMostSleepingGuard() => GetSleepingPeriods()
            .GroupBy(p => p.GuardId, p => p)
            .Select(ps => (guardId: ps.Key, totalMinutesAsleep: ps.Select(p => p.MidnightMinutes).Sum()))
            .MaxBy(p => p.totalMinutesAsleep)
            .guardId;

        public int FindMostSleepingMinuteOfGuard(int guardId) => GetSleepingPeriods()
            .Where(p => p.GuardId == guardId)
            .SelectMany(p => p.EnumerateMinutes())
            .ToDictionary(p => p, p => 1, p => p + 1)
            .MaxBy(kvp => kvp.Value)
            .Key;

        public (int gaurdId, int mostSleepingMinute) FindMostSleepingGuardInAMinute() => GetSleepingPeriods()
            .GroupBy(p => p.GuardId, p => p)
            .Select(ps => (guardId: ps.Key, sleepingMinutesCount: CountPeriodMinutes(ps)))
            .Select(p => (p.guardId, p.sleepingMinutesCount, mostSleepingMinute: p.sleepingMinutesCount.MaxByValue().Key))
            .MaxBy(p => p.sleepingMinutesCount[p.mostSleepingMinute])
            .Map(p => (p.guardId, p.mostSleepingMinute));

        public void AddAction(BeginsShift guardAction)
        {
            EndCurrentPeriodAndAdd(GetEndAtTime(guardAction));

            SetAwakePeriodFacotry(guardAction);
        }

        public void AddAction(FallsAsleep guardAction)
        {
            EndCurrentPeriodAndAdd(GetEndAtTime(guardAction));

            SetSleepPeriodFacotry(periods.Last().GuardId, guardAction);
        }

        public void AddAction(WakesUp guardAction)
        {
            EndCurrentPeriodAndAdd(GetEndAtTime(guardAction));

            SetAwakePeriodFacotry(periods.Last().GuardId, guardAction);
        }

        private DateTime GetEndAtTime(GuardAction guardAction) => guardAction.HappenedAt - OneMinute;

        private void EndCurrentPeriodAndAdd(DateTime happenedAt)
        {
            var guardTimePeriod = currentActionFactory(happenedAt);
            periods.Add(guardTimePeriod);
        }

        private void SetAwakePeriodFacotry(BeginsShift guardAction)
        {
            SetAwakePeriodFacotry(guardAction.GuardId, guardAction);
        }

        private void SetAwakePeriodFacotry(int guardId, GuardAction guardAction)
        {
            currentActionFactory = (to) => new GuardAwakePeriod(guardId, guardAction.HappenedAt, to);
        }

        private void SetSleepPeriodFacotry(int guardId, GuardAction guardAction)
        {
            currentActionFactory = (to) => new GuardAsleepPeriod(guardId, guardAction.HappenedAt, to);
        }

        private IEnumerable<GuardAsleepPeriod> GetSleepingPeriods() => periods.OfType<GuardAsleepPeriod>();

        private IDictionary<int, int> CountPeriodMinutes(IEnumerable<GuardTimePeriod> periods)
        {
            return periods.SelectMany(p => p.EnumerateMinutes()).ToDictionary(p => p, p => 1, p => p + 1);
        }
    }
}
