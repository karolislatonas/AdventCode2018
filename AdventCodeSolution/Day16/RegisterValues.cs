using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace AdventCodeSolution.Day16
{
    public class RegisterValues : ValueObject<RegisterValues>
    {
        public RegisterValues(IEnumerable<int> values) :
            this(values.ToArray())
        {

        }

        public RegisterValues(params int[] values) :
            this(ImmutableArray.Create(values.ToArray()))
        {

        }

        private RegisterValues(ImmutableArray<int> values)
        {
            Values = values;
        }

        public int this[int registerIndex] => Values[registerIndex];

        public ImmutableArray<int> Values { get; }

        public RegisterValues UpdateValue(int registerIndex, int newValue)
        {
            return new RegisterValues(
                Values.SetItem(registerIndex, newValue));
        }

        public static RegisterValues operator +(RegisterValues left, RegisterValues right) => left.CombineValues(right, (l, r) => l + r);

        public static RegisterValues operator -(RegisterValues left, RegisterValues right) => left.CombineValues(right, (l, r) => l - r);

        public static RegisterValues operator *(RegisterValues left, int multiplier) => new RegisterValues(left.Values.Select(v => v * multiplier));

        public override string ToString()
        {
            return Values
                .Select(v => v.ToString())
                .Aggregate((t, v) => $"{t} {v}");
        }

        protected override bool EqualsCore(RegisterValues other) => Values.SequenceEqual(other.Values);

        protected override int GetHashCodeCore() => Values.Aggregate(17, (t, c) => t ^ c);

        private RegisterValues CombineValues(RegisterValues other, Func<int, int, int> combineRegisterValues)
        {
            if (Values.Length != other.Values.Length)
                throw new InvalidOperationException();

            var combinedValues = Values.Select((v, i) => combineRegisterValues(v, other[i]));
            return new RegisterValues(combinedValues);
        }
    }
}
