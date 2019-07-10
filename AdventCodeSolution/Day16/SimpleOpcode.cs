using System;
using System.Collections.Generic;

namespace AdventCodeSolution.Day16
{
    public class SimpleOpcode : Opcode
    {
        public SimpleOpcode(string name, Func<IList<long>, RegisterInstruction, long> calculateNewRegisterValue) :
            base(name, calculateNewRegisterValue)
        {
        }

        public override bool IsComparison => throw new NotImplementedException();
    }
}
