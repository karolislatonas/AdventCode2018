using System;

namespace AdventCodeSolution.Day4.GuardActions
{
    public abstract class GuardAction
    {
        protected GuardAction(DateTime happenedAt)
        {
            HappenedAt = happenedAt;
        }

        public DateTime HappenedAt { get; }
    }
}
