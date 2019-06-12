using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Math;

namespace AdventCodeSolution.Day12
{
    public class PlantsGenerationCalculator
    {
        private readonly IReadOnlyList<PlantPot> initialState;
        private readonly IReadOnlyDictionary<string, bool> rules;

        public PlantsGenerationCalculator(IEnumerable<PlantPot> initialState, IEnumerable<KeyValuePair<string, bool>> rules)
        {
            this.initialState = initialState.ToArray();
            this.rules = rules.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public PlantPot[] GetPlantsOfGeneration(long generation)
        {
            if (generation < 0) throw new ArgumentOutOfRangeException(nameof(generation), "Generation cannot be lower than 0");

            var generations = EnumerateGenerations(generation).GetEnumerator();

            var currentGeneration = (long)0;
            var current = generations.Current;

            while (generations.MoveNext())
            {
                current = generations.Current;
                if (currentGeneration == generation)
                    return current;

                currentGeneration++;
            }

            return current;
        }

        private IEnumerable<PlantPot[]> EnumerateGenerations(long tillGeneration)
        {
            string ToPattern(IEnumerable<PlantPot> pots) => pots.Aggregate(new StringBuilder(), (t, c) => t.Append(c.PotSymbol)).ToString().Trim('.');

            var currentGeneration = initialState.ToArray();
            var currentGenerationPattern = ToPattern(currentGeneration);
            var currentGenerationNumber = (long)0;

            do
            {
                yield return currentGeneration;

                var nextGeneration = CreateNextGeneration(currentGeneration);
                var nextGenerationPattern = ToPattern(nextGeneration);

                var isSameAsPrevious = nextGenerationPattern == currentGenerationPattern;
                if (isSameAsPrevious)
                {
                    var timesToMoveDiff = tillGeneration - currentGenerationNumber;
                    var diffToAdd = timesToMoveDiff * (nextGeneration[0].Number - currentGeneration[0].Number);

                    yield return currentGeneration.Select(p => new PlantPot(p.Number + diffToAdd, p.ContainsPlant)).ToArray();
                    yield break;
                }

                currentGeneration = nextGeneration;
                currentGenerationPattern = nextGenerationPattern;
                currentGenerationNumber += 1; 

            } while (tillGeneration != currentGenerationNumber);

            yield return currentGeneration;
        }

        private PlantPot[] CreateNextGeneration(PlantPot[] currentGeneration)
        {
            var nextPlantGeneration = new List<PlantPot>(currentGeneration.Length + 4);

            for (var i = -2; i < currentGeneration.Length + 2; i++)
            {
                var pattern = GetPotNeighboursOfIndex(currentGeneration, i);

                if (!rules.TryGetValue(pattern, out var hasPotPlantInNextGeneration))
                {
                    hasPotPlantInNextGeneration = false;
                }

                var potNumber = GetPotNumberAtIndex(currentGeneration, i);
                nextPlantGeneration.Add(new PlantPot(potNumber, hasPotPlantInNextGeneration));
            }

            return nextPlantGeneration.SkipWhile(p => !p.ContainsPlant).ToArray();
        }

        private string GetPotNeighboursOfIndex(PlantPot[] plantPots, int index)
        {
            const int neigbourCountFromOneSide = 2;

            var startIndex = index - neigbourCountFromOneSide;
            var totalPots = neigbourCountFromOneSide * 2 + 1;

            var neighbourPlants = Enumerable
                .Range(startIndex, totalPots)
                .Select(i => plantPots.IsIndexInRange(index) ? plantPots[i].PotSymbol : PlantPot.EmptyPotSymbol)
                .ToArray();

            return new string(neighbourPlants);
        }

        private long GetPotNumberAtIndex(PlantPot[] plantPots, int index)
        {
            if (plantPots.IsIndexInRange(index))
            {
                return plantPots[index].Number;
            }

            var closestIndex = Min(Max(0, index), plantPots.Length - 1);

            return plantPots[closestIndex].Number - (closestIndex - index);
        }
    }
}
