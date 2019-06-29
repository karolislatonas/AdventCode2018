using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AdventCodeSolution.Day16
{
    public class RegisterValues : ValueObject<RegisterValues>
    {
        private readonly ImmutableArray<int> values;

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
            this.values = values;
        }

        public int this[int registerIndex] => values[registerIndex];

        public RegisterValues UpdateValue(int registerIndex, int newValue)
        {
            return new RegisterValues(
                values.SetItem(registerIndex, newValue));
        }

        protected override bool EqualsCore(RegisterValues other) => values.SequenceEqual(other.values);

        protected override int GetHashCodeCore() => values.Aggregate(17, (t, c) => t ^ c);
    }
}
