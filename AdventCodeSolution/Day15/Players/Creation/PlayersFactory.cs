using AdventCodeSolution.Day15.Players;
using AdventCodeSolution.Day15.Players.Creation;
using AdventCodeSolution.Day03;
using System;
using System.Collections.Generic;

namespace AdventCodeSolution.Day15.Players.Creation
{
    public class PlayersFactory
    {
        public static char ElfSymbol = 'E';
        public static char GoblinSymbol = 'G';

        private static readonly IReadOnlyDictionary<char, Func<XY, PlayerConfiguration, Player>> playersFactory =
            new Dictionary<char, Func<XY, PlayerConfiguration, Player>>()
            {
                [ElfSymbol] = (xy, config) => new Elf(xy, config.AttackPower),
                [GoblinSymbol] = (xy, config) => new Goblin(xy),
            };

        private readonly IDictionary<char, PlayerConfiguration> playersConfig;

        public PlayersFactory(PlayerConfiguration elfConfiguration) : this()
        {
            playersConfig.Add(ElfSymbol, elfConfiguration);
        }

        public PlayersFactory()
        {
            playersConfig = new Dictionary<char, PlayerConfiguration>();
        }

        public bool IsPlayerSymbol(char symbol) => playersFactory.ContainsKey(symbol);

        public Player CreatePlayer(char symbol, XY location)
        {
            var config = GetPlayerConfigurationOrDefault(symbol);

            return playersFactory[symbol].Invoke(location, config);
        }

        private PlayerConfiguration GetPlayerConfigurationOrDefault(char playerSymbol)
        {
            if(!playersConfig.TryGetValue(playerSymbol, out var playerConfig))
            {
                playerConfig = PlayerConfiguration.DefaultConfig;
            }

            return playerConfig;
        }
    }
}
