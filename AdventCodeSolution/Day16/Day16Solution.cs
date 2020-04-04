using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day16
{
    public class Day16Solution
    {
        public static void SolvePartOne()
        {
            var samples = GetSamplesInput();
            var opcodes = Opcode.GetAllOpcodes().ToArray();

            var result = samples.Count(r => FindMatchingOpcodes(r, opcodes).Length >= 3);

            result.WriteLine("Day 16, Part 1: ");
        }

        public static void SolvePartTwo()
        {
            var registerResults = GetSamplesInput();
            var instructions = GetTestInput();

            var opcodeNumbers = FindOpcodeNumbersMeaning(registerResults);

            var register = new RegisterValues(0, 0, 0, 0);

            foreach (var instruction in instructions)
                opcodeNumbers[instruction.OpcodeNumber].UpdateRegisters(register, instruction.Instruction);

            register[0].WriteLine("Day 16, Part 2: ");
        }

        private static Dictionary<int, Opcode> FindOpcodeNumbersMeaning(RegisterSample[] results)
        {
            var allOpcodes = Opcode.GetAllOpcodes().ToArray();
            var foundOpcodeNumbers = new Dictionary<int, Opcode>();

            do
            {
                var matchingOpcodes = results
                    .Where(r => !foundOpcodeNumbers.ContainsKey(r.OpcodeInstruction.OpcodeNumber))
                    .Select(r => 
                        (opcodes: FindMatchingOpcodes(r, allOpcodes).Where(o => !foundOpcodeNumbers.ContainsValue(o)).ToArray(), 
                         number: r.OpcodeInstruction.OpcodeNumber))
                    .Where(opcodes => opcodes.opcodes.Length == 1)
                    .Select(opcodes => (opcode: opcodes.opcodes.Single(), number: opcodes.number))
                    .ToArray();

                foundOpcodeNumbers.AddOrUpdateRange(matchingOpcodes, o => o.number, o => o.opcode, (@new, old) => old);

            } while (foundOpcodeNumbers.Count != allOpcodes.Length);

            return foundOpcodeNumbers;
        }

        private static Opcode[] FindMatchingOpcodes(RegisterSample result, Opcode[] opcodes)
        {
            return opcodes
                .Where(o => OpcodeMatches(result, o))
                .ToArray();
        }

        private static bool OpcodeMatches(RegisterSample sample, Opcode opcode)
        {
            var valuesAfterOpcodeApplied = sample.InitialValues.Copy();
            opcode.UpdateRegisters(valuesAfterOpcodeApplied, sample.OpcodeInstruction.Instruction);

            return valuesAfterOpcodeApplied.AreValuesEqual(sample.UpdatedValues);
        }

        private static RegisterSample[] GetSamplesInput()
        {
            var partsSeperator = Enumerable.Repeat(Environment.NewLine, 4).JoinIntoString();
            var resultsSeperator = Enumerable.Repeat(Environment.NewLine, 2).JoinIntoString();

            return InputResources.Day16Input.Split(partsSeperator)
                .First()
                .Split(resultsSeperator)
                .Select(ParseRegisterUpdateResult)
                .ToArray();
        }

        private static OpcodeInstruction[] GetTestInput()
        {
            var partsSeperator = Enumerable.Repeat(Environment.NewLine, 4).JoinIntoString();

            return InputResources.Day16Input.Split(partsSeperator)
                .Last()
                .Split(Environment.NewLine)
                .Select(ParseUpdateRegisterCommand)
                .ToArray();
        }

        private static RegisterSample ParseRegisterUpdateResult(string registerUpdateResultInput)
        {
            IEnumerable<int> ParseRegisterValues(string value, string prefix) => value
                .TrimStart(prefix)
                .Trim(' ', '[', ']')
                .Split(',')
                .Select(i => int.Parse(i.Trim()));

            var inputs = registerUpdateResultInput.Split(Environment.NewLine);

            var initialValues = new RegisterValues(ParseRegisterValues(inputs.First(), "Before:"));
            var updateCommand = ParseUpdateRegisterCommand(inputs[1]);
            var updatedValues = new RegisterValues(ParseRegisterValues(inputs.Last(), "After:"));

            return new RegisterSample(initialValues, updateCommand, updatedValues);
        }

        private static OpcodeInstruction ParseUpdateRegisterCommand(string input)
        {
            var commandInputs = input.Split(' ').Select(int.Parse).ToArray();

            return new OpcodeInstruction(commandInputs[0], commandInputs[1], commandInputs[2], commandInputs[3]);
        }
    }
}
