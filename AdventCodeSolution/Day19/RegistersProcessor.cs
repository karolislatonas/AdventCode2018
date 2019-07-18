using AdventCodeSolution.Day16;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCodeSolution.Day19
{
    public class RegistersProcessor
    {
        private readonly int boundedRegister;
        private readonly OpcodeInstruction[] instructions;

        public RegistersProcessor(OpcodeInstruction[] instructions, int boundedRegister)
        {
            this.boundedRegister = boundedRegister;
            this.instructions = instructions;
        }

        public RegisterValues Run(RegisterValues initialRegisterValues)
        {
            var currentPointer = 0;
            var previousPointer = currentPointer - 1;
            var registerValues = initialRegisterValues;

            var registerInPreviousPointers = new Dictionary<int, RegisterValues>();
            var loopingDiffs = new Dictionary<int, RegisterValues>();
            var hasLoopingDiffsChanged = false;

            //var writer = new StreamWriter(File.OpenWrite(@"C:\Users\KarolisL\Desktop\test1.txt"));
            var jumpIndex = 0;
            do
            {
                if (registerInPreviousPointers.TryGetValue(currentPointer, out var previousRegisters))
                {
                    var currentDiff = registerValues - previousRegisters;

                    if (loopingDiffs.TryGetValue(currentPointer, out var previousDiff))
                    {
                        hasLoopingDiffsChanged = hasLoopingDiffsChanged || !currentDiff.Equals(previousDiff);
                    }
                    else
                    {
                        hasLoopingDiffsChanged = true;
                    }

                    loopingDiffs[currentPointer] = currentDiff;
                }
                else
                {
                    hasLoopingDiffsChanged = true;
                }

                if (!hasLoopingDiffsChanged)
                {
                    //writer.WriteLine($"Jump: {jumpIndex}");

                    if(jumpIndex == 204)
                    {

                    }

                    var loopJump = CalculateLoopJump(previousPointer, currentPointer, loopingDiffs, registerInPreviousPointers);
                    registerValues += loopJump;

                    //writer.WriteLine($"{currentPointer} > {registerValues} (After Jump)");
                    //writer.WriteLine($"Jump finished: {jumpIndex}");

                    jumpIndex++;

                    loopingDiffs.Clear();
                    registerInPreviousPointers.Clear();
                }

                registerInPreviousPointers[currentPointer] = registerValues;

                hasLoopingDiffsChanged = false; // Reset

                previousPointer = currentPointer;

                (registerValues, currentPointer) = ApplyOpcode(registerValues, currentPointer);

                //writer.WriteLine($"{currentPointer} > {registerValues}");

            } while (currentPointer < instructions.Length);

            //writer.Flush();
            //writer.Dispose();

            return registerValues;
        }

        private RegisterValues CalculateLoopJump(
            int previousPointer,
            int currentPointer,
            Dictionary<int, RegisterValues> loopingDiffs,
            Dictionary<int, RegisterValues> registerInPreviousPointers)
        {
            var possibleJumps = loopingDiffs
                        .SelectMany(kvp => FindJumpTargets(registerInPreviousPointers[kvp.Key], kvp.Value))
                        .SelectMany(j => new[] { j - 1, j, j + 1 })
                        .Where(j => j >= 0)
                        .Distinct()
                        .OrderBy(v => v)
                        .ToArray();

            var jump = possibleJumps
                .First(j =>
                {
                    var initialPointer = previousPointer;

                    var jumpedValues = loopingDiffs.ToDictionary(kvp => kvp.Key, kvp => registerInPreviousPointers[kvp.Key] + kvp.Value * j);
                    var updatedValues = EnumerateOpcodeUpdates(jumpedValues[initialPointer], initialPointer)
                        .Take(loopingDiffs.Count)
                        .ToDictionary(r => r.pointer, r => r.values);

                        //!jumpedValues.TryGetValue(kvp.Key, out var otherValues) ||
                        //!loopingDiffs.TryGetValue(kvp.Key, out var loopingValue) ||
                        //(kvp.Value - otherValues) != loopingValue);

                    var diffsAreDifferent = updatedValues.Any(kvp =>
                    {
                        var missingJumpedValue = !jumpedValues.TryGetValue(kvp.Key, out var otherValues);
                        var missingLoopingDiff = !loopingDiffs.TryGetValue(kvp.Key, out var loopingDiff);

                        if (missingJumpedValue || missingJumpedValue)
                            return false;

                        var difference = kvp.Value - otherValues;

                        var diffsChanges = difference != loopingDiff;

                        return diffsChanges;
                    });

                    return diffsAreDifferent;
                });

            return loopingDiffs[currentPointer] * (jump - 1);
        }

        private IEnumerable<(RegisterValues values, int pointer)> EnumerateOpcodeUpdates(RegisterValues registerValues, int pointer)
        {
            return (registerValues, pointer)
                .StartEnumerate(v => ApplyOpcode(v.registerValues, v.pointer))
                .Skip(1)
                .TakeWhile(v => v.pointer < instructions.Length);
        }

        private (RegisterValues values, int pointer) ApplyOpcode(RegisterValues registerValues, int pointer)
        {
            registerValues = registerValues.UpdateValue(boundedRegister, pointer);

            registerValues = instructions[pointer].UpdateRegisters(registerValues);

            pointer = registerValues[boundedRegister] + 1;

            return (registerValues, pointer);
        }

        private static int[] FindJumpTargets(RegisterValues values, RegisterValues speed)
        {
            var channgingRegisters = speed.Values
                .Select((v, i) => (v, i))
                .Where(r => r.v != 0)
                .Select(r => r.i);

            var possibleJumps = channgingRegisters
                .SelectMany(registerIndex => Enumerable.Range(0, values.Values.Length)
                    .Select(i => (values[i] - values[registerIndex]) / speed[registerIndex]))
                .ToArray();

            return possibleJumps;
        }
    }
}
