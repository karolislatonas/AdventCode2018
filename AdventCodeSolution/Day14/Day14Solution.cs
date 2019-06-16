using System.Linq;

namespace AdventCodeSolution.Day14
{
    public class Day14Solution
    {
        public static void SolvePartOne()
        {
            var input = GetInput();
            var recipesLaboratory = new RecipesLaboratory(3, 7);

            var scores = recipesLaboratory.GetRecipesScoresAfter(input, 10);

            scores.JoinIntoString().WriteLine("Day 14, Part 1: ");
        }

        public static int GetInput() => int.Parse(InputResources.Day14Input);

        public static void SolvePartTwo()
        {
            var input = GetInput();
            var recipesLaboratory = new RecipesLaboratory(3, 7);

            var numbers = input.ToString().Select(c => int.Parse(c.ToString())).ToArray();

            var index = recipesLaboratory.FindIndexOfFirstOccurence(numbers);

            index.WriteLine("Day 14, Part 1: ");
        }
    }
}
