using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day2
{
    public class BoxIdDifference
    {
        public BoxIdDifference(string boxIdOne, string boxIdTwo)
        {
            BoxIdOne = boxIdOne;
            BoxIdTwo = boxIdTwo;
        }

        public string BoxIdOne { get; }

        public string BoxIdTwo { get; }

        public HashSet<int> GetIndexOfDifferences()
        {
            return BoxIdOne.Zip(BoxIdTwo, (c1, c2) => c1 == c2)
                .Select((isMatch, i) => new { IsMatch = isMatch, Index = i })
                .Where(m => !m.IsMatch)
                .Select(m => m.Index)
                .ToHashSet();
        }

        public string GetMatchingString()
        {
            var differences = GetIndexOfDifferences();

            return new string(BoxIdOne
                .Select((letter, i) => new { Letter = letter, Index = i })
                .Where(l => !differences.Contains(l.Index))
                .Select(l => l.Letter)
                .ToArray());
                
        }
    }
}
