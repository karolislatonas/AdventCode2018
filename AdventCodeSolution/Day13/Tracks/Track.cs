using AdventCodeSolution.Day3;

namespace AdventCodeSolution.Day13.Tracks
{
    public abstract class Track
    {
        protected Track(XY location)
        {
            Location = location;
        }

        public XY Location { get; }

        public abstract Cart TurnCart(Cart cart);
    }
}
