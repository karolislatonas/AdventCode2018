using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day16
{

    public class RegisterValues
    {
        private readonly int[] values;

        public RegisterValues(params int[] values) :
            this(values as IEnumerable<int>)
        {

        }

        public RegisterValues(IEnumerable<int> values)
        {
            this.values = values.ToArray();
        }

        public RegisterValues Copy() => new RegisterValues(values);

        public int this[int registerIndex] => Values[registerIndex];

        public IReadOnlyList<int> Values => values;

        public void UpdateValue(int registerIndex, int newValue)
        {
            values[registerIndex] = newValue;
        }

        public override string ToString() => string.Join(' ', values);

        public bool AreValuesEqual(RegisterValues other) => Values.SequenceEqual(other.Values);
    }
}
