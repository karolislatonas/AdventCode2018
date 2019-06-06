using static System.Math;

namespace AdventCodeSolution.Day7
{
    public class Job
    {
        public Job(int startedAtSecond, Step step)
        {
            StartedAtSecond = startedAtSecond;
            Step = step;
        }
        
        public int StartedAtSecond { get; }

        public Step Step { get; }

        public bool IsJobFinishedBy(int currentSecond) => SecondLeftToFinish(currentSecond) == 0;

        public int SecondLeftToFinish(int currentSecond) => Max(60 + StartedAtSecond + Step.SecondsTakesToConstruct - currentSecond, 0);

    }
}
