using AdventCodeSolution.Day03;
using System.Collections.Generic;

namespace AdventCodeSolution.Day18.Acres
{
    public abstract class Acre : ValueObject<Acre>
    {
        protected Acre(XY location)
        {
            Location = location;
        }

        public XY Location { get; }

        public abstract char Symbol { get; }

        public abstract Acre Transform(IEnumerable<Acre> neigbourAcres);

        protected override bool EqualsCore(Acre other)
        {
            return Location == other.Location &&
                GetType() == other.GetType();
        }

        protected override int GetHashCodeCore()
        {
            return Location.GetHashCode() ^ GetType().GetHashCode();
        }
    }
}
