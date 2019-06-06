using System;

namespace AdventCodeSolution.Day4.Periods
{
    public class GuardAwakePeriod : GuardTimePeriod
    {
        public GuardAwakePeriod(int guardId, DateTime from, DateTime to) : 
            base(guardId, from, to)
        {
        }
    }
}
