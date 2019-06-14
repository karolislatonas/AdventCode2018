using AdventCodeSolution.Day13.IntersectionStates;
using AdventCodeSolution.Day3;

namespace AdventCodeSolution.Day13
{
    public class Cart
    {
        private readonly IIntersectionState intersectionState;

        public Cart(XY location, XY direction) : 
            this(location, direction, TurnLeft.Value)
        {
            
        }

        private Cart(XY location, XY direction, IIntersectionState intersectionState)
        {
            Location = location;
            Direction = direction;
            this.intersectionState = intersectionState;
        }

        public XY Location { get; }

        public XY Direction { get; }

        public XY NextLocation => Location + Direction;

        public Cart MoveCart()
        {
            return new Cart(NextLocation, Direction, intersectionState);
        }

        public Cart TurnInDirection(XY newDirection)
        {
            return new Cart(Location, newDirection, intersectionState);
        }

        public Cart TurnInIntersection()
        {
            return new Cart(Location, intersectionState.ChangeDirection(Direction), intersectionState.NextState);
        }
    }
}
