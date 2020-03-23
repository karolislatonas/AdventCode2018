using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventCodeSolution.Day23
{
    public static class InputParser
    {
        public static Nanobot[] Parse(string input)
        {
            return input.Split(Environment.NewLine)
                .Select(ParseNanobot)
                .ToArray();
        }

        private static Nanobot ParseNanobot(string lineInput)
        {
            var splitInput = lineInput.Split(", ");
            var positionInput = splitInput[0].Trim("pos=<>".ToArray());
            var radiusInput = splitInput[1].Trim("r=".ToArray());

            return new Nanobot(
                XYZ.Parse(positionInput, ','),
                int.Parse(radiusInput));
        }

    }
}
