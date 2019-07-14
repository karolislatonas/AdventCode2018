using AdventCodeSolution.Day3;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day20
{
    public class PathParser
    {
        private const char StartSymbol = '^';
        private const char EndSymbol = '$';

        private static readonly IReadOnlyDictionary<char, XY> directionSymbols =
            new Dictionary<char, XY>()
            {
                ['N'] = XY.Up,
                ['E'] = XY.Right,
                ['S'] = XY.Down,
                ['W'] = XY.Left
            };

        public static Path Parse(string input)
        {
            var path = input
                .SkipWhile(s => s == StartSymbol)
                .TakeWhile(s => s != EndSymbol)
                .Aggregate(new Path(XY.Zero), ParseSymbolAndUpdateNode);

            return path.GetInitialPath();
        }

        private static Path ParseSymbolAndUpdateNode(Path current, char symbol)
        {
            switch (symbol)
            {
                case '(':
                    return current.ForkPathFromLastLocation();
                case ')':
                    return current.StartedFrom;
                case '|':
                    return current.StartedFrom.ForkPathFromLastLocation();
                default:
                    current.MoveInDirection(directionSymbols[symbol]);
                    return current;
            }
        }
    }
}
