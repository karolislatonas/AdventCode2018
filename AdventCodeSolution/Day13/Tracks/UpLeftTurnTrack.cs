using AdventCodeSolution.Day3;

namespace AdventCodeSolution.Day13.Tracks
{
    public class UpLeftTurnTrack : Track
    {
        public UpLeftTurnTrack(XY location) : base(location)
        {
        }

        public override Cart TurnCart(Cart cart)
        {
            var newDirection = new XY(-cart.Direction.Y, -cart.Direction.X);

            return cart.TurnInDirection(newDirection);
        }
    }
}
