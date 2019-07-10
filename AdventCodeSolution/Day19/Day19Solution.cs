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
            
            registerValues = Run(registerValues.UpdateValue(0, 0), instructions, boundedRegister);

            var result = registerValues[0];

            result.WriteLine("Day 19, Part 2: ");
        }

        private static RegisterValues Run(RegisterValues initialRegisterValues, OpcodeInstruction[] instructions, int boundedRegister)
        {
            var currentPointer = 0L;
            var previousPointer = -1L;
            var registerValues = initialRegisterValues;

            var initialFirst = registerValues[0];

            var registerInPreviousPointers = new Dictionary<long, RegisterValues>();
            var loopingDiffs = new Dictionary<long, long[]>();
            var hasLoopingDiffsChanged = false;

            //var writer = new StreamWriter(File.OpenWrite(@"C:\Users\KarolisL\Desktop\testQuick.txt"));
            var ind = 0;
            do
            {
                if(ind == 333)
                {

                }

                if (registerInPreviousPointers.TryGetValue(currentPointer, out var previousRegisters))
                {
                    var currentDiff = registerValues.Values.Select((v, i) => v - previousRegisters[i]).ToArray();

                    if (loopingDiffs.TryGetValue(currentPointer, out var previousDiff))
                    {
                        hasLoopingDiffsChanged = hasLoopingDiffsChanged || !currentDiff.SequenceEqual(previousDiff);
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
                    var registerThatWillChange = loopingDiffs.Keys
                        .Select(pointer => instructions[pointer])
                        .Where(i => i.Opcode is ComparisonOpcode)
                        .Select(i => (pointer: i.Pointer, changingValue: ((ComparisonOpcode)i.Opcode).GetRequiredValueToChange(registerValues, i.Instruction)))
                        .ToArray();

                    var pointerThatWillBeReachedFirst = registerThatWillChange
                        .Select(x =>
                        {
                            var speed = loopingDiffs[x.pointer][x.changingValue.register];
                            return (x.pointer, loopsRequires: ((x.changingValue.value - registerValues[x.changingValue.register]) / speed) - speed);
                        })
                        .MinBy(x => x.loopsRequires);

                    var start = Math.Max(pointerThatWillBeReachedFirst.loopsRequires, 1);

                    var jumpSize = BinarySearch.ForawrdSearchLong(start, v => v * 2, jumper =>
                    {
                        var jumpedValues = loopingDiffs.ToDictionary(kvp => kvp.Key,
                                kvp => registerInPreviousPointers[kvp.Key].AddValues(
                                    new RegisterValues(kvp.Value).MultiplyValuesBy(jumper)));

                        var pointer = previousPointer;
                        var registers = jumpedValues[pointer];
                        var pointAndRegisters = (pointer, registers);
                        var newRegisters = pointAndRegisters.StartEnumerate(p =>
                        {
                            if (p.pointer >= instructions.Length)
                                return (p.pointer, null);

                            var newR = p.registers.UpdateValue(boundedRegister, p.pointer);
                            newR = instructions[p.pointer].UpdateRegisters(newR);
                            var ptr = newR[boundedRegister] + 1;

                            return (ptr, newR);
                        }).Skip(1)
                            .TakeWhile((pr, i) => pr.registers != null && i < jumpedValues.Count)
                            .ToDictionary(pr => pr.pointer, pr => pr.registers);

                        var diffsAreSame = newRegisters.All(kvp =>
                            jumpedValues.TryGetValue(kvp.Key, out var otherValues) &&
                                kvp.Value.Values.Select((v, i) => v - otherValues[i])
                                   .Select((d, i) => d - loopingDiffs[kvp.Key][i]).All(v => v == 0));

                        return diffsAreSame ? 1 : -1;
                    });

                    var jump = new RegisterValues(loopingDiffs[currentPointer]).MultiplyValuesBy(jumpSize);
                    var prev = registerValues;
                    registerValues = registerValues.AddValues(jump);

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

        //private static RegisterValues Run(RegisterValues initialRegisterValues, OpcodeInstruction[] instructions, int boundedRegister)
        //{
        //    var currentPointer = 0L;
        //    var registerValues = initialRegisterValues;

        //    do
        //    {
        //        registerValues = registerValues.UpdateValue(boundedRegister, currentPointer);

        //        registerValues = instructions[currentPointer].UpdateRegisters(registerValues);

        //        currentPointer = registerValues[boundedRegister] + 1;

        //    } while (currentPointer < instructions.Length);

        //    return registerValues;
        //}
    }
}
