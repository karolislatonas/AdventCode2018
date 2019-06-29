namespace AdventCodeSolution.Day16
{
    public class OpcodeInstruction
    {
        public OpcodeInstruction(int opcodeNumber, int valueA, int valueB, int outputToRegister)
        {
            OpcodeNumber = opcodeNumber;
            Instruction = new RegisterInstruction(valueA, valueB, outputToRegister);
        }

        public int OpcodeNumber { get; }

        public RegisterInstruction Instruction { get; }
    }
}
