using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace AdventCodeSolution.Day16
{
    public class RegisterValues : ValueObject<RegisterValues>
    {
        private readonly ImmutableArray<long> values;

        public RegisterValues(IEnumerable<int> values) :
            this(values.Select(i => (long)i).ToArray())
        {

        }

        public RegisterValues(IEnumerable<long> values) :
            this(values.ToArray())
        {

        }

        public RegisterValues(params long[] values) :
            this(ImmutableArray.Create(values))
        {

        }

        private RegisterValues(ImmutableArray<long> values)
        {
            this.values = values;
        }

        public IList<long> Values => values;

        public long this[int registerIndex] => values[registerIndex];
        
        public RegisterValues UpdateValue(int registerIndex, long newValue)
        {
            return new RegisterValues(
                values.SetItem(registerIndex, newValue));
        }

        public RegisterValues MultiplyValuesBy(long multiplier)
        {
            return new RegisterValues(values.Select(v => v * multiplier));
        }

        public RegisterValues AddValues(RegisterValues otherRegisterValues)
        {
            var addedValues = values.Select((v, i) => otherRegisterValues[i] + v);

            return new RegisterValues(addedValues);
        }

        public override string ToString()
        {
            return values.Length > 0 ?
                values.Aggregate(new StringBuilder(), (b, v) => b.Append(' ').Append(v)).ToString() :
                string.Empty;
        }

        protected override bool EqualsCore(RegisterValues other) => values.SequenceEqual(other.values);

        protected override int GetHashCodeCore() => (int)values.Aggregate(17L, (t, c) => t ^ c);
    }
}
