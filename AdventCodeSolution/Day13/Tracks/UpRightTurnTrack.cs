using AdventCodeSolution.Day03;

namespace AdventCodeSolution.Day13.Tracks
{
    public class UpRightTurnTrack : Track
    {
        public UpRightTurnTrack(XY location) : base(location)
        {
        }

        public override Cart TurnCart(Cart cart)
        {
            var newDirection = new XY(cart.Direction.Y, cart.Direction.X);

            return cart.TurnInDirection(newDirection);
        }
    }
}
