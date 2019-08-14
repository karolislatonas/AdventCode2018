using AdventCodeSolution.Day03;
using System;
using System.Collections.Generic;

namespace AdventCodeSolution.Day22
{
    public class CaveSearchComparerByCostAndRemainignPathLength : IComparer<CaveSearch>
    {
        private readonly XY targetLocation;

        public CaveSearchComparerByCostAndRemainignPathLength(XY targetLocation)
        {
            this.targetLocation = targetLocation;
        }

        public int Compare(CaveSearch x, CaveSearch y)
        {
            var xCompareValue = CalculatCompareValue(x);
            var yCompareValue = CalculatCompareValue(y);

            return Math.Sign(xCompareValue - yCompareValue);
        }

        private int CalculatCompareValue(CaveSearch search)
        {
            return search.Cost + search.Location.GetManhattanDistance(targetLocation);
        }
    }
}
