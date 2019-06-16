using AdventCodeSolution.Day15.Players;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day15
{
    public class Battle
    {
        private readonly BattleMap map;
        private readonly PlayerActionMaker playerActionMaker;

        public Battle(BattleMap map)
        {
            this.map = map;
            playerActionMaker = new PlayerActionMaker(map);
        }

        public IReadOnlyCollection<Player> Players => map.Players;

        public int TotalFullMovesMade { get; private set; }

        public void Simulate()
        {
            while (!IsBattleFinished())
            {
                MakeMoveOfPlayers();
            }
        }

        private void MakeMoveOfPlayers()
        {
            var players = map.Players.ToArray();

            foreach (var player in players)
            {
                if (IsBattleFinished())
                    return;

                playerActionMaker.MakeActionForPlayer(player);
            }

            TotalFullMovesMade++;
        }

        private bool IsBattleFinished() => map.ContainsOnlyOneRacePlayers();
    }
}
