using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day07
{
    public class JobQueue
    {
        private readonly HashSet<char> finishedSteps = new HashSet<char>();
        private readonly LinkedList<Step> stepsToDo;
        private readonly HashSet<Job> jobsCurrentlyExecuting = new HashSet<Job>();
        private readonly int workersCount;

        public JobQueue(int workersCount, IEnumerable<Step> stepsToDo)
        {
            this.workersCount = workersCount;
            this.stepsToDo = stepsToDo.ToLinkedList();
        }

        public int TotalSecondsPassed { get; private set; }

        public bool HasUnfinishedSteps => jobsCurrentlyExecuting.Count > 0 || stepsToDo.Count > 0;

        public void PassTimeTillNextFinishedStep()
        {
            TotalSecondsPassed += GetRemainingSecondsForStepToBeFinished(TotalSecondsPassed);

            var finishedJobs = jobsCurrentlyExecuting.Where(j => j.IsJobFinishedBy(TotalSecondsPassed)).ToArray();

            foreach (var finishedJob in finishedJobs)
            {
                finishedSteps.Add(finishedJob.Step.StepId);
                jobsCurrentlyExecuting.Remove(finishedJob);
            } 
        }

        public void StartNextAvailableSteps()
        {
            foreach (var nextAvailableStep in GetNextStepAvailableToExecute())
            {
                stepsToDo.Remove(nextAvailableStep);
                jobsCurrentlyExecuting.Add(new Job(TotalSecondsPassed, nextAvailableStep.Value));
            }
        }

        private int GetRemainingSecondsForStepToBeFinished(int currentTime)
        {
            return jobsCurrentlyExecuting
                    .Select(j => j.SecondLeftToFinish(currentTime))
                    .AggregateOrDefault(s => s.Min(), 0);
        }

        private LinkedListNode<Step>[] GetNextStepAvailableToExecute()
        {
            return jobsCurrentlyExecuting.Count < workersCount ?
                stepsToDo.EnumerateNodes().Where(n => AllDependenciesFinished(n.Value)).ToArray() :
                new LinkedListNode<Step>[0];
        }

        private bool AllDependenciesFinished(Step step) => step.Dependencies.All(finishedSteps.Contains);
    }
}
