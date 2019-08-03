using AdventCodeSolution.Day03;

namespace AdventCodeSolution.Day13.IntersectionStates
{
    public interface IIntersectionState
    {
        IIntersectionState NextState { get; }

        XY ChangeDirection(XY direction);
    }
}
