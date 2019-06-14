using System;
using AdventCodeSolution.Day3;

namespace AdventCodeSolution.Day13.IntersectionStates
{
    public class TurnRight : IIntersectionState
    {
        private TurnRight()
        {

        }

        public IIntersectionState NextState => TurnLeft.Value;

        public XY ChangeDirection(XY direction) => new XY(direction.Y, -direction.X);

        public static TurnRight Value { get; } = new TurnRight();
    }
}
