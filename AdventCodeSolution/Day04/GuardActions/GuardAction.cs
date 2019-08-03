using System;

namespace AdventCodeSolution.Day04.GuardActions
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
