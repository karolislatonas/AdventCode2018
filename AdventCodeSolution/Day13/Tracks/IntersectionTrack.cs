using AdventCodeSolution.Day3;

namespace AdventCodeSolution.Day13.Tracks
{
    public class IntersectionTrack : Track
    {
        public IntersectionTrack(XY location) : base(location)
        {
        }

        public override Cart TurnCart(Cart cart) => cart.TurnInIntersection();
    }
}
