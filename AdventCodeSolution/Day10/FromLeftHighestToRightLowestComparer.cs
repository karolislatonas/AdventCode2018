using AdventCodeSolution.Day03;
using System;
using System.Collections.Generic;

namespace AdventCodeSolution.Day10
{
    public class FromLeftHighestToRightLowestComparer : IComparer<XY>
    {
        private FromLeftHighestToRightLowestComparer()
        {

        }

        public int Compare(XY left, XY right)
        {
            if (left.Y > right.Y) return -1;
            if (left.Y < right.Y) return 1;

            return Math.Sign(left.X - right.X);
        }

        public static FromLeftHighestToRightLowestComparer Value { get; } = new FromLeftHighestToRightLowestComparer();
    }
}
