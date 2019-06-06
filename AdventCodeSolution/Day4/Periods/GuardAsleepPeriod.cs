using System;

namespace AdventCodeSolution.Day4.Periods
{
    public class GuardAsleepPeriod : GuardTimePeriod
    {
        public GuardAsleepPeriod(int guardId, DateTime from, DateTime to) : 
            base(guardId, from, to)
        {
        }
    }
}
