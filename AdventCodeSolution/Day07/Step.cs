using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day07
{
    public class Step
    {
        private const char FirstStep = 'A';

        private readonly HashSet<char> dependencies;

        public Step(char stepId) : 
            this(stepId, Enumerable.Empty<char>())
        {

        }

        public Step(char stepId, char initialDependency) :
            this(stepId, initialDependency.RepeatOnce())
        {

        }

        public Step(char stepId, IEnumerable<char> dependencies)
        {
            StepId = stepId;
            this.dependencies = dependencies.ToHashSet();
        }

        public char StepId { get; }

        public IReadOnlyCollection<char> Dependencies => dependencies;

        public bool DependsOn(char dependency) => dependencies.Contains(dependency);

        public int SecondsTakesToConstruct => StepId - (FirstStep - 1);

        public bool CanBeExecuted => dependencies.Count == 0;

        public void RemoveDependency(char dependency)
        {
            dependencies.Remove(dependency);
        }

        public void AddDependency(char dependency)
        {
            dependencies.Add(dependency);
        }

    }
}
