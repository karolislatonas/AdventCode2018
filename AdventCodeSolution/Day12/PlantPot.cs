using System;

namespace AdventCodeSolution.Day12
{
    public class PlantPot
    {
        private const char PotWithPlantSymbol = '#';
        public const char EmptyPotSymbol = '.';

        public PlantPot(int number, bool containsPlant)
        {
            Number = number;
            ContainsPlant = containsPlant;
            PotSymbol = ContainsPlant ? PotWithPlantSymbol : EmptyPotSymbol;
        }

        public int Number { get; }

        public bool ContainsPlant { get; }

        public char PotSymbol { get; }

        public static bool ParseContainsPlant(char pot)
        {
            switch (pot)
            {
                case PotWithPlantSymbol: return true;
                case EmptyPotSymbol: return false;
                default: throw new ArgumentException($"Expected values: . or #, but was {pot}", nameof(pot));
            }
        }
    }
}
