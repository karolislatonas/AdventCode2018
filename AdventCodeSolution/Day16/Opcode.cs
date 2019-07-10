using System;
using System.Collections.Generic;

namespace AdventCodeSolution.Day16
{
    public abstract class Opcode 
    {
        private readonly Func<IList<long>, RegisterInstruction, long> calculateNewRegisterValue;

        protected Opcode(string name, Func<IList<long>, RegisterInstruction, long> calculateNewRegisterValue)
        {
            Name = name;
            this.calculateNewRegisterValue = calculateNewRegisterValue;
        }

        public string Name { get; }

        public abstract bool IsComparison { get; }

        public RegisterValues UpdateRegisters(RegisterValues registerValues, RegisterInstruction updateCommand)
        {
            var newRegisterValue = calculateNewRegisterValue(registerValues.Values, updateCommand);

            return registerValues.UpdateValue(updateCommand.OutputToRegister, newRegisterValue);
        }

        public void UpdateRegisters(long[] registerValues, RegisterInstruction updateCommand)
        {
            registerValues[updateCommand.OutputToRegister] = calculateNewRegisterValue(registerValues, updateCommand);
        }

        public static Opcode Addr { get; } = new SimpleOpcode("addr", (v, c) => v[c.ValueA] + v[c.ValueB]);

        public static Opcode Addi { get; } = new SimpleOpcode("addi", (v, c) => v[c.ValueA] + c.ValueB);

        public static Opcode Mulr { get; } = new SimpleOpcode("mulr", (v, c) => v[c.ValueA] * v[c.ValueB]);

        public static Opcode Muli { get; } = new SimpleOpcode("muli", (v, c) => v[c.ValueA] * c.ValueB);

        public static Opcode Banr { get; } = new SimpleOpcode("banr", (v, c) => v[c.ValueA] & v[c.ValueB]);

        public static Opcode Bani { get; } = new SimpleOpcode("bani", (v, c) => v[c.ValueA] & c.ValueB);

        public static Opcode Borr { get; } = new SimpleOpcode("borr", (v, c) => v[c.ValueA] | v[c.ValueB]);

        public static Opcode Bori { get; } = new SimpleOpcode("bori", (v, c) => v[c.ValueA] | c.ValueB);

        public static Opcode Setr { get; } = new SimpleOpcode("setr", (v, c) => v[c.ValueA]);

        public static Opcode Seti { get; } = new SimpleOpcode("seti", (v, c) => c.ValueA);

        public static Opcode Gtir { get; } = new SimpleOpcode("gtir", (v, c) => c.ValueA > v[c.ValueB] ? 1 : 0);

        public static Opcode Gtri { get; } = new ComparisonOpcode("gtri", (v, c) => v[c.ValueA] > c.ValueB ? 1 : 0, (v, c) => c.ValueB + 1);

        public static Opcode Gtrr { get; } = new ComparisonOpcode("gtrr", (v, c) => v[c.ValueA] > v[c.ValueB] ? 1 : 0, (v, c) => v[c.ValueB] + 1);

        public static Opcode Eqir { get; } = new SimpleOpcode("eqir", (v, c) => c.ValueA == v[c.ValueB] ? 1 : 0);

        public static Opcode Eqri { get; } = new ComparisonOpcode("eqri", (v, c) => v[c.ValueA] == c.ValueB ? 1 : 0, (v, c) => c.ValueB);

        public static Opcode Eqrr { get; } = new ComparisonOpcode("eqrr", (v, c) => v[c.ValueA] == v[c.ValueB] ? 1 : 0, (v, c) => v[c.ValueB]);

        public static IEnumerable<Opcode> GetAllOpcodes()
        {
            yield return Addr;
            yield return Addi;
            yield return Mulr;
            yield return Muli;
            yield return Banr;
            yield return Bani;
            yield return Borr;
            yield return Bori;
            yield return Setr;
            yield return Seti;
            yield return Gtir;
            yield return Gtri;
            yield return Gtrr;
            yield return Eqir;
            yield return Eqri;
            yield return Eqrr;
        }
    }
}
