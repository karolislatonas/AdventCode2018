using System;
using System.Collections.Generic;

namespace AdventCodeSolution.Day16
{
    public class ComparisonOpcode : Opcode
    {
        private readonly Func<IList<long>, RegisterInstruction, long> requiredValueToChange;

        public ComparisonOpcode(string name, Func<IList<long>, RegisterInstruction, long> calculateNewRegisterValue,
            Func<IList<long>, RegisterInstruction, long> requiredValueToChange) : 
            base(name, calculateNewRegisterValue)
        {
            this.requiredValueToChange = requiredValueToChange;
        }

        public override bool IsComparison => throw new NotImplementedException();

        public (int register, long value) GetRequiredValueToChange(RegisterValues registerValues, RegisterInstruction instruction)
        {
            var value = requiredValueToChange(registerValues.Values, instruction);

            return (instruction.ValueA, value);
        }
    }
}
