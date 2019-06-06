using System.Collections.Generic;

namespace AdventCodeSolution.Day5
{
    public class Polymer
    {
        private readonly List<char> units = new List<char>() { default };
        private readonly IUnitFilter unitFilter;

        public Polymer() : this(NoneFilter.Value)
        {

        }

        public Polymer(IUnitFilter unitFilter)
        {
            this.unitFilter = unitFilter;
        }

        public void AddUnit(char unit)
        {
            if (!unitFilter.CanBeAdded(unit))
            {
                return;
            }

            var adjacentUnit = units[units.Count - 1];

            if (AreUnitsOfOppositePolarity(adjacentUnit, unit))
            {
                RemoveLast(); 
            }
            else
            {
                units.Add(unit);
            }
        }

        public int Length => units.Count - 1;

        private void RemoveLast()
        {
            units.RemoveAt(units.Count - 1);
        }

        private bool AreUnitsOfOppositePolarity(char leftUnit, char rightUnit)
        {
            if (leftUnit == rightUnit) return false;

            if (char.ToUpper(leftUnit) == rightUnit) return true;
            if (char.ToLower(leftUnit) == rightUnit) return true;

            return false;
        }
    }
}
