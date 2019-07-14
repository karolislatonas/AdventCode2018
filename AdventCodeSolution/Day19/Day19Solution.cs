using AdventCodeSolution.Day16;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCodeSolution.Day19
{
    public static class Day19Solution
    {
        public static void SolvePartOne()
        {
            var (boundedRegister, instructions) = InstructionParser.ParseInstructions(GetInput());

            var initialRegisterValues = new RegisterValues(Enumerable.Repeat(0, 6).ToArray());

            var registerValues = Run(initialRegisterValues, instructions, boundedRegister);

            var result = registerValues[0];

            result.WriteLine("Day 19, Part 1: ");
        }

        public static void SolvePartTwo()
        {
            var (boundedRegister, instructions) = InstructionParser.ParseInstructions(GetInput());

            var registerValues = new RegisterValues(Enumerable.Repeat(0, 6));
            
            registerValues = Run(registerValues.UpdateValue(0, 1), instructions, boundedRegister);

            var result = registerValues[0];

            result.WriteLine("Day 19, Part 2: ");
        }

        private static RegisterValues Run(RegisterValues initialRegisterValues, OpcodeInstruction[] instructions, int boundedRegister)
        {
            var currentPointer = 0;
            var previousPointer = currentPointer - 1;
            var registerValues = initialRegisterValues;
            
            var registerInPreviousPointers = new Dictionary<int, RegisterValues>();
            var loopingDiffs = new Dictionary<int, RegisterValues>();
            var hasLoopingDiffsChanged = false;

            //var writer = new StreamWriter(File.OpenWrite(@"C:\Users\KarolisL\Desktop\test1.txt"));
            var ind = 0;
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
                    var possibleJumps = loopingDiffs
                        .SelectMany(kvp => FindJumps(registerInPreviousPointers[kvp.Key], kvp.Value))
                        .SelectMany(j => new[] { j - 1, j })
                        .Distinct()
                        .OrderBy(v => v)
                        .ToArray();

                    var jump = possibleJumps
                        .First(j =>
                        {
                            var initialPointer = previousPointer;

                            var jumpedValues = loopingDiffs.ToDictionary(kvp => kvp.Key, kvp => registerInPreviousPointers[kvp.Key] + kvp.Value * j);
                            var updatedValues = EnumerateOpcodeUpdates(jumpedValues[initialPointer], instructions, boundedRegister, initialPointer)
                                .Take(loopingDiffs.Count)
                                .ToDictionary(r => r.pointer, r => r.values, (t, old) => t.values);

                            var diffsAreDifferent = updatedValues.Any(kvp => 
                                !jumpedValues.TryGetValue(kvp.Key, out var otherValues) ||
                                !loopingDiffs.TryGetValue(kvp.Key, out var loopingValue) ||
                                (kvp.Value - otherValues) != loopingValue);

                            return diffsAreDifferent;
                        });

                    registerValues += loopingDiffs[currentPointer] * (jump - 1);

                    loopingDiffs.Clear();
                    registerInPreviousPointers.Clear();
                }

                hasLoopingDiffsChanged = false; // Reset

                registerInPreviousPointers[currentPointer] = registerValues;

                registerValues = registerValues.UpdateValue(boundedRegister, currentPointer);

                registerValues = instructions[currentPointer].UpdateRegisters(registerValues);

                previousPointer = currentPointer;

                currentPointer = registerValues[boundedRegister] + 1;

                //writer.WriteLine($"{currentPointer} -> {registerValues}");

                ind++;

            } while (currentPointer < instructions.Length);

            //writer.Flush();
            //writer.Dispose();

            return registerValues;
        }

        private static string GetInput() => InputResources.Day19Input;

        private static int[] FindJumps(RegisterValues values, RegisterValues speed)
        {
            var channgingRegisters = speed.Values
                .Select((v, i) => (v, i))
                .Where(r => r.v != 0)
                .Select(r => r.i);

            var possibleJumps = channgingRegisters
                .SelectMany(registerIndex => Enumerable.Range(0, values.Values.Length)
                    .Select(i => (values[i] - values[registerIndex]) / speed[registerIndex])
                    .Where(j => j > 0))
                .Distinct()
                .ToArray();

            return possibleJumps;
        }

        private static IEnumerable<(int pointer, RegisterValues values)> EnumerateOpcodeUpdates(
            RegisterValues initialRegisterValues, 
            OpcodeInstruction[] instructions, 
            int boundedRegister, 
            int initialPointer)
        {
            var currentPointer = initialPointer;
            var registerValues = initialRegisterValues;

            do
            {
                registerValues = registerValues.UpdateValue(boundedRegister, currentPointer);

                registerValues = instructions[currentPointer].UpdateRegisters(registerValues);

                currentPointer = registerValues[boundedRegister] + 1;

                yield return (currentPointer, registerValues);

            } while (currentPointer < instructions.Length);
        }
    }
}
