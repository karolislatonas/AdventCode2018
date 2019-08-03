using AdventCodeSolution.Day13.Tracks;
using AdventCodeSolution.Day03;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day13
{
    public class TracksCartsParser
    {
        private readonly static HashSet<char> leftRightSymbols = new HashSet<char>() { '>', '<', '-', '+' };
        private readonly static HashSet<char> upDownSymbols = new HashSet<char>() { '^', 'v', '|', '+' };

        public static readonly IReadOnlyDictionary<char, Func<XY, Cart>> cartFactories =
            new Dictionary<char, Func<XY, Cart>>
            {
                ['>'] = location => new Cart(location, XY.Right),
                ['<'] = location => new Cart(location, XY.Left),
                ['^'] = location => new Cart(location, XY.Up),
                ['v'] = location => new Cart(location, XY.Down),
            };

        public static (Track[] tracks, Cart[] carts)  Parse(string input)
        {
            var tracksByLocation = input.Split(Environment.NewLine)
                .ReverseEnumerate()
                .SelectMany((line, y) => line.Select((t, x) => (location: new XY(x, y), symbol: t)))
                .ToDictionary(t => t.location, t => t.symbol);

            var tracks = tracksByLocation
                .Select(kvp => ParseTrackOrDefault(kvp.Key, kvp.Value, tracksByLocation))
                .WhereNotNull();

            var carts = tracksByLocation.Where(kvp => cartFactories.ContainsKey(kvp.Value))
                .Select(kvp => cartFactories[kvp.Value].Invoke(kvp.Key));

            return (tracks: tracks.ToArray(), carts: carts.ToArray());
        }

        private static Track ParseTrackOrDefault(XY location, char symbol, IReadOnlyDictionary<XY, char> tracksByLocation)
        {
            switch (symbol)
            {
                case '<':
                case '>':
                case '-':
                case 'v':
                case '^':
                case '|': return new StraightTrack(location);

                case '+': return new IntersectionTrack(location);

                case '\\' when IsUpLeft(location, tracksByLocation): return new UpLeftTurnTrack(location);
                case '\\' when IsDownLeft(location, tracksByLocation): return new DownLeftTurnTrack(location);

                case '/' when IsUpRight(location, tracksByLocation): return new UpRightTurnTrack(location);
                case '/' when IsDownRight(location, tracksByLocation): return new DownRightTurnTrack(location);

                default: return symbol == ' ' ? (Track)null: throw new Exception();
            }
        }

        private static bool IsUpLeft(XY location, IReadOnlyDictionary<XY, char> tracksByLocation)
        {
            var isLeftCharCorrect = tracksByLocation.TryGetValue(location + XY.Left, out var leftSymbol) ?
                leftRightSymbols.Contains(leftSymbol) :
                false;

            var isDownCharCorrect = tracksByLocation.TryGetValue(location + XY.Down, out var downSymbol) ?
                upDownSymbols.Contains(downSymbol) :
                false;

            return isLeftCharCorrect && isDownCharCorrect;
        }

        private static bool IsUpRight(XY location, IReadOnlyDictionary<XY, char> tracksByLocation)
        {
            var isRightCharCorrect = tracksByLocation.TryGetValue(location + XY.Right, out var rightSymbol) ?
                leftRightSymbols.Contains(rightSymbol) :
                false;

            var isDownCharCorrect = tracksByLocation.TryGetValue(location + XY.Down, out var downSymbol) ?
                upDownSymbols.Contains(downSymbol) :
                false;

            return isRightCharCorrect && isDownCharCorrect;
        }

        private static bool IsDownLeft(XY location, IReadOnlyDictionary<XY, char> tracksByLocation)
        {
            var isRightCharCorrect = tracksByLocation.TryGetValue(location + XY.Right, out var rightSymbol) ?
                leftRightSymbols.Contains(rightSymbol) :
                false;

            var isUpCharCorrect = tracksByLocation.TryGetValue(location + XY.Up, out var upSymbol) ?
                upDownSymbols.Contains(upSymbol) :
                false;

            return isRightCharCorrect && isUpCharCorrect;
        }

        private static bool IsDownRight(XY location, IReadOnlyDictionary<XY, char> tracksByLocation)
        {
            var isLeftCharCorrect = tracksByLocation.TryGetValue(location + XY.Left, out var leftSymbol) ?
                leftRightSymbols.Contains(leftSymbol) :
                false;

            var isUpCharCorrect = tracksByLocation.TryGetValue(location + XY.Up, out var upSymbol) ?
                upDownSymbols.Contains(upSymbol) :
                false;

            return isLeftCharCorrect && isUpCharCorrect;
        }
    }
}
