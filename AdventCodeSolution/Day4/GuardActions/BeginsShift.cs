using System;

namespace AdventCodeSolution.Day4.GuardActions
{
    public class BeginsShift : GuardAction
    {
        public BeginsShift(int guardId, DateTime happenedOn) : base(happenedOn)
        {
            GuardId = guardId;
        }

        public int GuardId { get; }
    }
}
