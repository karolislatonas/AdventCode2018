using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day9
{
    public class MarbleGame
    {
        private const int SpecialMarbleNumber = 23;
        private const int MarbleToSkipOnSpecial = 7;

        private readonly int playersCount;
        private readonly int totalMarbles;
        private readonly Dictionary<int, long> playersScore = new Dictionary<int, long>();

        private int currentPlayer = 0;
        private int currentMarbleNumber = 1;

        private readonly LinkedList<int> placedMarbles = new LinkedList<int>(0.RepeatOnce());
        private LinkedListNode<int> currentMarble;

        public MarbleGame(int playersCount, int totalMarbles)
        {
            this.playersCount = playersCount;
            this.totalMarbles = totalMarbles;

            currentMarble = placedMarbles.First;
        }

        public bool IsGameFinished => currentMarbleNumber > totalMarbles;

        public long GetPlayerWithHighestScore() => playersScore.Values.Max();

        public void AddNextMarble()
        {
            if (IsGameFinished) throw new InvalidOperationException("Game is finished");

            if(currentMarbleNumber % SpecialMarbleNumber == 0)
            {
                OnSpecialMarble();
            }
            else
            {
                var nextMarble = NextOfCurrentOrHead();
                currentMarble = placedMarbles.AddAfter(nextMarble, currentMarbleNumber);
            }

            MoveToNextPlayerAndMarbleNumber();
        }

        private void OnSpecialMarble()
        {
            var marbleToTake = placedMarbles.ReverseEnumerateInfinitelyFrom(currentMarble).Skip(MarbleToSkipOnSpecial).First();

            currentMarble = NextOrHead(marbleToTake);

            placedMarbles.Remove(marbleToTake);

            AddToPlayersScore(currentPlayer, currentMarbleNumber + marbleToTake.Value);
        }

        private LinkedListNode<int> NextOfCurrentOrHead() => NextOrHead(currentMarble);

        private LinkedListNode<int> NextOrHead(LinkedListNode<int> node) => node.Next ?? placedMarbles.First;

        private void MoveToNextPlayerAndMarbleNumber()
        {
            currentMarbleNumber++;
            currentPlayer = (currentPlayer + 1) % playersCount;
        }

        private void AddToPlayersScore(int player, int diff)
        {
            playersScore.AddOrUpdate(player, p => diff, (p, s) => s + diff);
        }

    }
}
