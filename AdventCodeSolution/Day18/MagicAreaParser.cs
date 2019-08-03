using AdventCodeSolution.Day18.Acres;
using AdventCodeSolution.Day03;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day18
{
    public static class MagicAreaParser
    {
        private static readonly IReadOnlyDictionary<char, Func<XY, Acre>> acreFactories = new Dictionary<char, Func<XY, Acre>>()
        {
            ['.'] = l => new OpenGround(l),
            ['#'] = l => new Lumberyard(l),
            ['|'] = l => new Tree(l)
        };

        public static MagicArea Parse(string magicAreaString)
        {
            var acres = magicAreaString.Split(Environment.NewLine)
                .SelectMany((line, y) => line.Select((s, x) => CreateAcre(s, new XY(x, -y))));

            return new MagicArea(acres);
        }

        private static Acre CreateAcre(char symbol, XY location)
        {
            return acreFactories[symbol].Invoke(location);
        }

    }
}
