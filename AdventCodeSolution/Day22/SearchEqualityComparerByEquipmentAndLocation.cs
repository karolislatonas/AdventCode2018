using System;
using System.Collections.Generic;
using System.Text;

namespace AdventCodeSolution.Day22
{
    public class SearchEqualityComparerByEquipmentAndLocation : IEqualityComparer<CaveSearch>
    {
        private SearchEqualityComparerByEquipmentAndLocation()
        {

        }

        public bool Equals(CaveSearch x, CaveSearch y)
        {
            var isTrue = x.Equipment == y.Equipment &&
                x.Location == y.Location;

            if(isTrue)
            {

            }

            return isTrue;
        }

        public int GetHashCode(CaveSearch obj)
        {
            return obj.Location.GetHashCode() ^
                obj.Equipment.GetHashCode();
        }

        public static SearchEqualityComparerByEquipmentAndLocation Value { get; } =
            new SearchEqualityComparerByEquipmentAndLocation();
    }
}
