using AdventCodeSolution.Day16;

namespace AdventCodeSolution.Day19
{
    public class OpcodeInstruction
    {
        public OpcodeInstruction(int pointer, Opcode opcode, RegisterInstruction instruction)
        {
            Pointer = pointer;
            Opcode = opcode;
            Instruction = instruction;
        }

        public int Pointer { get; }

        public Opcode Opcode { get; }

        public RegisterInstruction Instruction { get; }

        public void UpdateRegisters(RegisterValues registerValues)
        {
            Opcode.UpdateRegisters(registerValues, Instruction);
        }
    }
}
