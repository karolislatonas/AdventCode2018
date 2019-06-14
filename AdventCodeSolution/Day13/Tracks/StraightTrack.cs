using AdventCodeSolution.Day3;

namespace AdventCodeSolution.Day13.Tracks
{
    public class StraightTrack : Track
    {
        public StraightTrack(XY location) : base(location)
        {
        }

        public override Cart TurnCart(Cart cart) => cart;
    }
}
