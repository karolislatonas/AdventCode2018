using AdventCodeSolution.Day03;

namespace AdventCodeSolution.Day13.IntersectionStates
{
    public class TurnLeft : IIntersectionState
    {
        private TurnLeft()
        {

        }

        public IIntersectionState NextState => MoveStraight.Value;

        public XY ChangeDirection(XY direction) => new XY(-direction.Y, direction.X);

        public static TurnLeft Value { get; } = new TurnLeft();
    }
}
