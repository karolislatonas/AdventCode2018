using System.Linq;

namespace AdventCodeSolution.Day22.SearchActions
{
    public class ComposedAction : IAction
    {
        private readonly IAction[] actionsToApply;

        public ComposedAction(params IAction[] actionsToApply)
        {
            this.actionsToApply = actionsToApply;
        }

        public CaveSearch ApplyAction(CaveSearch caveSearch)
        {
            return actionsToApply.Aggregate(caveSearch, (c, a) => a.ApplyAction(c));
        }
    }
}
