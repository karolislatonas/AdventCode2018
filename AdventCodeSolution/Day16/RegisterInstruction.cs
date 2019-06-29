namespace AdventCodeSolution.Day16
{
    public class RegisterInstruction
    {
        public RegisterInstruction(int valueA, int valueB, int outputToRegister)
        {
            ValueA = valueA;
            ValueB = valueB;
            OutputToRegister = outputToRegister;
        }

        public int ValueA { get; }

        public int ValueB { get; }

        public int OutputToRegister { get; }
    }
}
