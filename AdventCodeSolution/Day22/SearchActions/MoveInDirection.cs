using AdventCodeSolution.Day03;

namespace AdventCodeSolution.Day22.SearchActions
{
    public class MoveInDirection : IAction
    {
        private readonly XY movementDirection;

        public MoveInDirection(XY movementDirection)
        {
            this.movementDirection = movementDirection;
        }

        public CaveSearch ApplyAction(CaveSearch caveSearch) => caveSearch.Move(movementDirection);
    }
}
