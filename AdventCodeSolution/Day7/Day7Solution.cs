using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day7
{
    public class Day7Solution
    {
        public static void SolvePartOne()
        {
            var steps = GetInput();

            var stepsExecutionOrder = FindStepsExecutionOrder(steps.Values);

            new string(stepsExecutionOrder).WriteLine("Day 7, Part One: ");
        }

        public static void SolvePartTwo()
        {
            var steps = GetInput();

            var stepsToExecute = FindStepsExecutionOrder(steps.Values).Select(s => steps[s]);

            var jobQueue = new JobQueue(5, stepsToExecute);

            while (jobQueue.HasUnfinishedSteps)
            {
                jobQueue.StartNextAvailableSteps();
                jobQueue.PassTimeTillNextFinishedStep();
            }

            jobQueue.TotalSecondsPassed.WriteLine("Day 7, Part Two: ");
        }

        private static char[] FindStepsExecutionOrder(IEnumerable<Step> stepsSequence)
        {
            var steps = stepsSequence.ToDictionary(s => s.StepId, s => new Step(s.StepId, s.Dependencies));

            var stepsWithoutDependencies = steps.Values.Where(s => s.Dependencies.Count == 0).Select(s => s.StepId);

            var availableStepsForExecution = new SortedSet<char>(stepsWithoutDependencies);

            var stepsByDependencies = steps.Values.Aggregate(new Dictionary<char, HashSet<char>>(),
                (dependencies, step) =>
                {
                    foreach (var dependency in step.Dependencies)
                        dependencies.AddOrUpdate(dependency,
                            key => new HashSet<char>() { step.StepId },
                            (key, value) => { value.Add(step.StepId); return value; });

                    return dependencies;
                });

            var executedSteps = new List<Step>();

            while (availableStepsForExecution.Count != 0)
            {
                var executedStep = steps[availableStepsForExecution.First()];
                executedSteps.Add(executedStep);

                availableStepsForExecution.Remove(executedStep.StepId);

                if (stepsByDependencies.TryGetValue(executedStep.StepId, out var stepsThatMightBeExecuted))
                {
                    foreach (var step in stepsThatMightBeExecuted.Select(id => steps[id]).ToArray())
                    {
                        step.RemoveDependency(executedStep.StepId);
                        if (step.CanBeExecuted)
                        {
                            stepsThatMightBeExecuted.Remove(step.StepId);
                            availableStepsForExecution.Add(step.StepId);
                        }

                    }
                }
            }

            return executedSteps.Select(s => s.StepId).ToArray();
        }

        public static Dictionary<char, Step> GetInput() => InputResources.Day7Input
            .Split(Environment.NewLine)
            .Select(i => i.TrimStart("Step ").TrimEnd(" can begin."))
            .Select(i => (stepId: i.Last(), dependsOnStepId: i.First()))
            .Aggregate(new Dictionary<char, Step>(),
                (steps, step) =>
                {
                    steps.AddOrUpdate(step.stepId,
                        key => new Step(key, step.dependsOnStepId),
                        (key, value) => { value.AddDependency(step.dependsOnStepId); return value; });

                    steps.AddOrUpdate(step.dependsOnStepId,
                        key => new Step(key),
                        (key, value) => value);

                    return steps;
                });
    }
}
