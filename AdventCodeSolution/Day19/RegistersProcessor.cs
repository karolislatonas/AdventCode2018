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

        public void Run(RegisterValues registerValues)
        {
            var pointer = 0;

            do
            {
                registerValues.UpdateValue(boundedRegister, pointer);

                instructions[pointer].UpdateRegisters(registerValues);

                pointer = registerValues[boundedRegister] + 1;

            } while (pointer < instructions.Length);
        }
    }
}
