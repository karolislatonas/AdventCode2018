using System;
using System.Collections.Generic;

namespace AdventCodeSolution.Day4.Periods
{
    public abstract class GuardTimePeriod
    {
        protected GuardTimePeriod(int guardId, DateTime from, DateTime to)
        {
            GuardId = guardId;
            From = from;
            To = to;
        }

        public int GuardId { get; }

        public DateTime From { get; }

        public DateTime To { get; }

        public int MidnightMinutes => GetMidnightMinutes();

        public IEnumerable<int> EnumerateMinutes()
        {
            var (from, _) = GetMidnightTime();
            var startOffset = (int)from.TimeOfDay.TotalMinutes;

            var totalMinutes = MidnightMinutes;

            for (var i = 0; i < totalMinutes; i++)
            {
                yield return i + startOffset;
            }
        }

        public bool ContainsMinute(int minute)
        {
            var (from, to) = GetMidnightTime();

            var midnightTime = to.Date + TimeSpan.FromMinutes(minute);

            return from <= midnightTime && midnightTime <= to;
        }

        private int GetMidnightMinutes()
        {
            var (from, to) = GetMidnightTime();

            return (int)(to - from).TotalMinutes + 1;
        }

        protected (DateTime from, DateTime to) GetMidnightTime()
        {
            var midnightToTime = TimeSpan.FromMinutes(Math.Min(To.TimeOfDay.TotalMinutes, TimeSpan.FromHours(1).TotalMinutes));

            var toDateOnly = To.Date;

            return (From > toDateOnly ? From : toDateOnly,
                toDateOnly + midnightToTime);
        }
    }
}
