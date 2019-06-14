using AdventCodeSolution.Day3;

namespace AdventCodeSolution.Day13.IntersectionStates
{
    public class MoveStraight : IIntersectionState
    {
        private MoveStraight()
        {

        }

        public IIntersectionState NextState => TurnRight.Value;

        public XY ChangeDirection(XY direction) => direction;

        public static MoveStraight Value { get; } = new MoveStraight();
    }
}
