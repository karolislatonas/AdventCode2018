namespace AdventCodeSolution.Day09
{
    public class Day9Solution
    {
        public static void SolvePartOne()
        {
            var marbleGame = new MarbleGame(424, 71482);

            while (!marbleGame.IsGameFinished)
            {
                marbleGame.AddNextMarble();
            }

            marbleGame.GetPlayerWithHighestScore().WriteLine("Day 9, Part One: ");
        }

        public static void SolvePartTwo()
        {
            var marbleGame = new MarbleGame(424, 7148200);

            while (!marbleGame.IsGameFinished)
            {
                marbleGame.AddNextMarble();
            }

            marbleGame.GetPlayerWithHighestScore().WriteLine("Day 9, Part Two: ");
        }
    }
}
