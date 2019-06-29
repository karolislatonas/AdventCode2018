namespace AdventCodeSolution.Day16
{
    public class RegisterSample
    {
        public RegisterSample(RegisterValues initialValues, OpcodeInstruction opcodeInstruction, RegisterValues updatedValues)
        {
            InitialValues = initialValues;
            OpcodeInstruction = opcodeInstruction;
            UpdatedValues = updatedValues;
        }

        public RegisterValues InitialValues { get; }

        public OpcodeInstruction OpcodeInstruction { get; }

        public RegisterValues UpdatedValues { get; }
    }
}
