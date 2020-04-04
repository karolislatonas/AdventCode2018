using AdventCodeSolution.Day16;
using System.Linq;

namespace AdventCodeSolution.Day19
{
    public static class Day19Solution
    {
        public static void SolvePartOne()
        {
            var (boundedRegister, instructions) = InstructionParser.ParseInstructions(GetInput());

            var registerValues = new RegisterValues(Enumerable.Repeat(0, 6).ToArray());
            var registerProcessor = new RegistersProcessor(instructions, boundedRegister);

            registerProcessor.Run(registerValues);

            var result = registerValues[0];

            result.WriteLine("Day 19, Part 1: ");
        }

        public static void SolvePartTwo()
        {
            var target = 10551417;

            var firstRegisterValue = 1;
            var fifthRegisterValue = 2;

            while (firstRegisterValue < target)
            {
                if(target % fifthRegisterValue == 0)
                    firstRegisterValue += fifthRegisterValue;

                fifthRegisterValue++;
            }

            firstRegisterValue.WriteLine("Day 19, Part 2: ");
        }

        private static string GetInput() => InputResources.Day19Input;

    }
}
