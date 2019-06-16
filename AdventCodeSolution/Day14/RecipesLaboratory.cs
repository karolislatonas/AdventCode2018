using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day14
{
    public class RecipesLaboratory
    {
        private readonly int firstStartingRecipeScore;
        private readonly int secondStartingRecipeScore;

        public RecipesLaboratory(int firstStartingRecipeScore, int secondStartingRecipeScore)
        {
            this.firstStartingRecipeScore = firstStartingRecipeScore;
            this.secondStartingRecipeScore = secondStartingRecipeScore;
        }

        public int[] GetRecipesScoresAfter(int count, int recipesCount)
        {
            var recipeScoreChanges = EnumerateRecipesScoreChanges().First(r => r.recipeScores.Count >= count + recipesCount);

            return recipeScoreChanges.recipeScores.TakeFrom(count, recipesCount).ToArray();
        }

        public int FindIndexOfFirstOccurence(int[] scores)
        {
            var recipeScoresChanges = EnumerateRecipesScoreChanges()
                .First(r => Enumerable.Range(0, r.changedBy).Any(offset => LastElementAreEqualTo(r.recipeScores, scores, offset)));

            var offsetS = Enumerable.Range(0, recipeScoresChanges.changedBy).First(offset => LastElementAreEqualTo(recipeScoresChanges.recipeScores, scores, offset));

            return recipeScoresChanges.recipeScores.Count - offsetS - scores.Length;
        }

        public IEnumerable<(IReadOnlyList<int> recipeScores, int changedBy)> EnumerateRecipesScoreChanges()
        {
            var recipeScores = new List<int>()
            {
                firstStartingRecipeScore,
                secondStartingRecipeScore
            };

            var firstElfRecipeIndex = 0;
            var secondElfRecipeIndex = 1;

            while (true)
            {
                var totalScore = recipeScores[firstElfRecipeIndex] + recipeScores[secondElfRecipeIndex];

                var newRecipes = SplitIntoNumbers(totalScore);

                var previousScoresCount = recipeScores.Count;
                recipeScores.AddRange(newRecipes);

                firstElfRecipeIndex = GetNewIndexOfRecipe(recipeScores, firstElfRecipeIndex);
                secondElfRecipeIndex = GetNewIndexOfRecipe(recipeScores, secondElfRecipeIndex);

                yield return (recipeScores, recipeScores.Count - previousScoresCount);
            }
        }

        private int GetNewIndexOfRecipe(IList<int> recipeScores, int currentRecipreIndex)
        {
            return MoveIndexFrom(recipeScores, currentRecipreIndex, recipeScores[currentRecipreIndex] + 1);
        }

        private int MoveIndexFrom<T>(IList<T> numbers, int fromIndex, int count)
        {
            return (fromIndex + count) % numbers.Count;
        }

        private IEnumerable<int> SplitIntoNumbers(int number)
        {
            var seperatedNumbers = new List<int>();

            do
            {
                seperatedNumbers.Add(number % 10);
                number /= 10;

            } while (number != 0);

            return seperatedNumbers.ReverseEnumerate();
        }

        private bool LastElementAreEqualTo<T>(IReadOnlyList<T> list, T[] ending, int offset)
        {
            if (list.Count - offset < ending.Length) return false;

            return list.ReverseEnumerate()
                .Skip(offset)
                .Take(ending.Length)
                .Select((e, i) => (listElement: e, endingElement: ending[ending.Length - 1 - i]))
                .All(elements => elements.endingElement.Equals(elements.listElement));
        }
    }
}
