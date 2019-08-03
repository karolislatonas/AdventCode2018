using System;

namespace AdventCodeSolution.Day04.GuardActions
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
