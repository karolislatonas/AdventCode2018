using AdventCodeSolution.Day16;

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
            var pointer = 0;
            var registerValues = initialRegisterValues;

            do
            {
                registerValues = registerValues.UpdateValue(boundedRegister, pointer);

                registerValues = instructions[pointer].UpdateRegisters(registerValues);

                pointer = registerValues[boundedRegister] + 1;

            } while (pointer < instructions.Length);

            return registerValues;
        }
    }
}
