using AdventCodeSolution.Day15.Players.Creation;
using AdventCodeSolution.Day03;
using System;
using System.Linq;

namespace AdventCodeSolution.Day15
{
    public class BattleMapParser
    {
        private const char WallSymbol = '#';
        private readonly PlayersFactory playersFactory;

        public BattleMapParser(PlayersFactory playersFactory)
        {
            this.playersFactory = playersFactory;
        }

        public BattleMap Parse(string input)
        {
            var symbolLocations = input.Split(Environment.NewLine)
                .ReverseEnumerate()
                .SelectMany((line, y) => line.Select((c, x) => (symbol: c, location: new XY(x, y))));

            var players = symbolLocations
                .Where(sl => playersFactory.IsPlayerSymbol(sl.symbol))
                .Select(sl => playersFactory.CreatePlayer(sl.symbol, sl.location));

            var wallLocations = symbolLocations
                .Where(sl => sl.symbol == WallSymbol)
                .Select(sl => sl.location);

            return new BattleMap(wallLocations, players);
        }

    }
}
