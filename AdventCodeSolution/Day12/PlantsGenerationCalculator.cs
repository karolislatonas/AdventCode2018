using System;
using System.Collections.Generic;
using System.Linq;
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

            var generations = EnumerateGenerations().GetEnumerator();

            var currentGeneration = (long)0;
            while (true)
            {
                generations.MoveNext();

                var current = generations.Current;
                if (currentGeneration == generation)
                    return current;

                currentGeneration++;
            }
        }

        private IEnumerable<PlantPot[]> EnumerateGenerations()
        {
            var currentGeneration = initialState.ToArray();

            do
            {
                yield return currentGeneration;

                var nextPlantGeneration = new List<PlantPot>((int)(currentGeneration.Length * 1.10));
                for(var i = -2; i < currentGeneration.Length + 1; i++)
                {
                    var pattern = GetPotNeighboursOfIndex(currentGeneration, i);

                    if(!rules.TryGetValue(pattern, out var hasPotPlantInNextGeneration))
                    {
                        hasPotPlantInNextGeneration = false;
                    }   

                    var potNumber = GetPotNumber(currentGeneration, i);
                    nextPlantGeneration.Add(new PlantPot(potNumber, hasPotPlantInNextGeneration));
                }

                currentGeneration = nextPlantGeneration.SkipWhile(p => !p.ContainsPlant).ToArray();

            } while (true);
        }

        private string GetPotNeighboursOfIndex(PlantPot[] plantPots, int index)
        {
            bool IsIndexInRange(int i) => 0 <= i && i < plantPots.Length;

            var neighbourPlants = Enumerable.Range(index - 2, 5)
                .Select(i => IsIndexInRange(i) ? plantPots[i].PotSymbol : PlantPot.EmptyPotSymbol)
                .ToArray();

            return new string(neighbourPlants);
        }

        private int GetPotNumber(PlantPot[] plantPots, int index)
        {
            bool IsIndexInRange(int i) => 0 <= i && i < plantPots.Length;

            if (IsIndexInRange(index))
            {
                return plantPots[index].Number;
            }

            var closestIndex = Min(Max(0, index), plantPots.Length - 1);

            return plantPots[closestIndex].Number - (closestIndex - index);
        }
    }
}
