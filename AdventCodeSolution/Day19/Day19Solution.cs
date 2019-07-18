using AdventCodeSolution.Day16;
using System.Linq;

namespace AdventCodeSolution.Day19
{
    public static class Day19Solution
    {
        public static void SolvePartOne()
        {
            var (boundedRegister, instructions) = InstructionParser.ParseInstructions(GetInput());

            var initialRegisterValues = new RegisterValues(Enumerable.Repeat(0, 6).ToArray());
            var registerProcessor = new RegistersProcessor(instructions, boundedRegister);

            var registerValues = registerProcessor.Run(initialRegisterValues);

            var result = registerValues[0];

            result.WriteLine("Day 19, Part 1: ");
        }

        public static void SolvePartTwo()
        {
            var (boundedRegister, instructions) = InstructionParser.ParseInstructions(GetInput());

            var registerValues = new RegisterValues(Enumerable.Repeat(0, 6));
            
            var registerProcessor = new RegistersProcessor(instructions, boundedRegister);

            registerValues = registerProcessor.Run(registerValues.UpdateValue(0, 1));

            var result = registerValues[0];

            result.WriteLine("Day 19, Part 2: ");
        }

        private static string GetInput() => InputResources.Day19Input;

    }
}
