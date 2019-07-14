using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

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

        public RegisterValues MultiplyValuesBy(int multiplier)
        {
            return new RegisterValues(Values.Select(v => v * multiplier));
        }

        public RegisterValues AddValues(RegisterValues otherRegisterValues)
        {
            var addedValues = Values.Select((v, i) => otherRegisterValues[i] + v);

            return new RegisterValues(addedValues);
        }


        protected override bool EqualsCore(RegisterValues other) => Values.SequenceEqual(other.Values);

        protected override int GetHashCodeCore() => Values.Aggregate(17, (t, c) => t ^ c);
    }
}
