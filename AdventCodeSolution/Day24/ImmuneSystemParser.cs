using System;
using System.Linq;

namespace AdventCodeSolution.Day24
{
    public class ImmuneSystemParser
    {   
        private static readonly string UnitGroupSeperator = Environment.NewLine;
        private static readonly string[] UnitGroupDataSeperators = new[]
        {
            "units each with",
            "hit points",
            "with an attack that does",
            "damage at initiative"
        };

        public static ImmuneSystem Parse(string input)
        {
            var groupsInputs = input.Split(GetImminitiesAndInfectionsSeparator());

            var immunities = ParseUnitGroups(groupsInputs[0]);
            var infections = ParseUnitGroups(groupsInputs[1]);

            return new ImmuneSystem(immunities, infections);
        }

        private static UnitsGroup[] ParseUnitGroups(string input)
        {
            return input
                .Split(UnitGroupSeperator)
                .Skip(1) // skip Immune System: or Infection: headers
                .Select(ParseUnitGroup)
                .ToArray();
        }

        private static UnitsGroup ParseUnitGroup(string input)
        {
            var splitInput = input
                .Split(UnitGroupDataSeperators, StringSplitOptions.None)
                .Select(i => i.Trim(' '))
                .ToArray();

            var unitsCount = int.Parse(splitInput[0]);
            var unitHitPoints = int.Parse(splitInput[1]);
            var immunityDetails = ParseImmunityDetails(splitInput[2]);
            var unitAttackDetails = ParseUnitAttackDetails(splitInput[3]);
            var initiative = int.Parse(splitInput[4]);

            return new UnitsGroup(unitsCount, unitHitPoints, unitAttackDetails, immunityDetails, initiative);
        }

        private static UnitAttackDetails ParseUnitAttackDetails(string input)
        {
            var splitInput = input
                .Split(' ')
                .ToArray();

            var unitDamage = int.Parse(splitInput[0]);
            var attackType = ParseAttackType(splitInput[1]);

            return new UnitAttackDetails(unitDamage, attackType);
        }

        private static ImmunityDetails ParseImmunityDetails(string input)
        {
            if (string.IsNullOrEmpty(input))
                return ImmunityDetails.Empty;

            var splitInput = input
                .Trim('(', ')')
                .Split(';');

            var weakToAttacks = ParseImmunityWeakness(splitInput.SingleOrDefault(i => i.Contains("weak")));
            var immuneToAttacks = ParseImmunityWeakness(splitInput.SingleOrDefault(i => i.Contains("immune")));

            return new ImmunityDetails(weakToAttacks, immuneToAttacks);
        }

        private static AttackType[] ParseImmunityWeakness(string input)
        {
            if (string.IsNullOrEmpty(input))
                return Array.Empty<AttackType>();

            return input.Split("to")[1]
                        .Split(',')
                        .Select(i => i.Trim())
                        .Select(ParseAttackType)
                        .ToArray();
        }

        private static AttackType ParseAttackType(string input)
        {
            return Enum.Parse<AttackType>(input, true);
        }

        private static string GetImminitiesAndInfectionsSeparator()
        {
            return $"{UnitGroupSeperator}{Environment.NewLine}";
        }
    }
}
