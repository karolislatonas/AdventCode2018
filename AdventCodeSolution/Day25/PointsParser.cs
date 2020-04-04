using System;
using System.Linq;

namespace AdventCodeSolution.Day25
{
    public class PointsParser
    {
        public static NPoint[] Parse(string input)
        {
            return input
                .Split(Environment.NewLine)
                .Select(NPoint.Parse)
                .ToArray();
        }
    }
}
