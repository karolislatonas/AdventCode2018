using AdventCodeSolution.Day3;

namespace AdventCodeSolution.Day13.Tracks
{
    public class DownRightTurnTrack : Track
    {
        public DownRightTurnTrack(XY location) : base(location)
        {
        }

        public override Cart TurnCart(Cart cart)
        {
            var newDirection = new XY(cart.Direction.Y, cart.Direction.X);

            return cart.TurnInDirection(newDirection);
        }
    }
}
