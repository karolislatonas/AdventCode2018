using System;
using System.Collections.Generic;

namespace AdventCodeSolution.Day16
{
    public class Opcode
    {
        private readonly Func<IList<int>, RegisterInstruction, int> calculateNewRegisterValue;

        protected Opcode(string name, Func<IList<int>, RegisterInstruction, int> calculateNewRegisterValue)
        {
            Name = name;
            this.calculateNewRegisterValue = calculateNewRegisterValue;
        }

        public string Name { get; }

        public RegisterValues UpdateRegisters(RegisterValues registerValues, RegisterInstruction updateCommand)
        {
            var newRegisterValue = calculateNewRegisterValue(registerValues.Values, updateCommand);

            return registerValues.UpdateValue(updateCommand.OutputToRegister, newRegisterValue);
        }

        public static Opcode Addr { get; } = new Opcode("addr", (v, c) => v[c.ValueA] + v[c.ValueB]);

        public static Opcode Addi { get; } = new Opcode("addi", (v, c) => v[c.ValueA] + c.ValueB);

        public static Opcode Mulr { get; } = new Opcode("mulr", (v, c) => v[c.ValueA] * v[c.ValueB]);

        public static Opcode Muli { get; } = new Opcode("muli", (v, c) => v[c.ValueA] * c.ValueB);

        public static Opcode Banr { get; } = new Opcode("banr", (v, c) => v[c.ValueA] & v[c.ValueB]);

        public static Opcode Bani { get; } = new Opcode("bani", (v, c) => v[c.ValueA] & c.ValueB);

        public static Opcode Borr { get; } = new Opcode("borr", (v, c) => v[c.ValueA] | v[c.ValueB]);

        public static Opcode Bori { get; } = new Opcode("bori", (v, c) => v[c.ValueA] | c.ValueB);

        public static Opcode Setr { get; } = new Opcode("setr", (v, c) => v[c.ValueA]);

        public static Opcode Seti { get; } = new Opcode("seti", (v, c) => c.ValueA);

        public static Opcode Gtir { get; } = new Opcode("gtir", (v, c) => c.ValueA > v[c.ValueB] ? 1 : 0);

        public static Opcode Gtri { get; } = new Opcode("gtri", (v, c) => v[c.ValueA] > c.ValueB ? 1 : 0);

        public static Opcode Gtrr { get; } = new Opcode("gtrr", (v, c) => v[c.ValueA] > v[c.ValueB] ? 1 : 0);

        public static Opcode Eqir { get; } = new Opcode("eqir", (v, c) => c.ValueA == v[c.ValueB] ? 1 : 0);

        public static Opcode Eqri { get; } = new Opcode("eqri", (v, c) => v[c.ValueA] == c.ValueB ? 1 : 0);

        public static Opcode Eqrr { get; } = new Opcode("eqrr", (v, c) => v[c.ValueA] == v[c.ValueB] ? 1 : 0);

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
