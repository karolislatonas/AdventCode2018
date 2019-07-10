using AdventCodeSolution.Day16;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day19
{
    public static class InstructionParser
    {
        private static readonly IReadOnlyDictionary<string, Opcode> opcodes =
            Opcode.GetAllOpcodes().ToDictionary(c => c.Name, c => c);

        public static (int pointer, OpcodeInstruction[] instructions) ParseInstructions(string input)
        {
            var instructions = input.Split(Environment.NewLine);

            var opcodeInstructions = instructions.Skip(1).Select((l, i) => ParseInstruction(i, l)).ToArray();
            var initialPointer = int.Parse(instructions.First().Split(' ').Last());

            return (initialPointer, opcodeInstructions);
        }

        private static OpcodeInstruction ParseInstruction(int pointer, string instructionLine)
        {
            var inputs = instructionLine.Split(' ');
            var instructionInputs = inputs.Skip(1).Select(int.Parse).ToArray();
            var opcodeName = inputs[0];

            return new OpcodeInstruction(
                pointer,
                opcodes[opcodeName],
                new RegisterInstruction(instructionInputs[0], instructionInputs[1], instructionInputs[2]));
        }
    }
}
