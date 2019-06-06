using System;
using System.Linq;

namespace AdventCodeSolution.Day2
{
    public class Day2Solution
    {
        public static void SolvePartOne()
        {
            var init = (twoLettersCount: 0, threeLettersCount: 0);

            var letterCounts = GetInput()
                .Select(CalculateRepeatingLetters)
                .Aggregate(init, (total, current) =>
                    (total.twoLettersCount + current.twoLettersCount,
                    total.threeLettersCount + current.threeLettersCount));

            var result = letterCounts.twoLettersCount * letterCounts.threeLettersCount;

            result.WriteLine("Day 2. Part 1: ");
        }

        public static void SolvePartTwo()
        {
            var result = string.Empty;

            var input = GetInput();

            var differentId = input.SelectMany(boxId => input.Where(otherBoxId => otherBoxId != boxId)
                    .Select(otherBoxId => new BoxIdDifference(boxId, otherBoxId))
                    .Where(b => b.GetIndexOfDifferences().Count == 1))
                    .FirstOrDefault();

            differentId.GetMatchingString().WriteLine("Day 2. Part 2: ");
        }

        private static (int twoLettersCount, int threeLettersCount) CalculateRepeatingLetters(string boxId)
        {
            var characters = boxId.ToDictionary(c => c, c => 1, v => v + 1);

            return (characters.Any(kvp => kvp.Value == 2) ? 1 : 0,
                characters.Any(kvp => kvp.Value == 3) ? 1 : 0);
        }

        private static string[] GetInput() => InputResources.Day2Input.Split(Environment.NewLine);
    }
}
