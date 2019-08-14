using AdventCodeSolution.Day03;
using AdventCodeSolution.Day22.Equipments;
using AdventCodeSolution.Day22.Regions;
using AdventCodeSolution.Day22.SearchActions;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace AdventCodeSolution.Day22
{
    public class PathFinder
    {
        private static readonly XY[] MoveDirections = new [] { XY.Up, XY.Left, XY.Right, XY.Down };
        private static readonly IEquipment[] Equipments = new IEquipment[]
        {
            ClimbingGear.Value,
            Torch.Value,
            NoneEquipment.Value
        };

        public CaveSearch FindQuickestPath(Cave cave)
        {
            var targetSearch = new CaveSearch(cave.TargetLocation, Torch.Value);
            var initialSearch = new CaveSearch(cave.InitialLocation, Torch.Value);
            var caveSearchesToExpand = initialSearch.RepeatOnce().ToHashSet(SearchEqualityComparerByEquipmentAndLocation.Value);
            var reachedSearches = initialSearch.RepeatOnce().ToHashSet(SearchEqualityComparerByEquipmentAndLocation.Value);
            var searchComparer = new CaveSearchComparerByCostAndRemainignPathLength(cave.TargetLocation);

            CaveSearch currentSearchToTarget = null;

            do
            {
                // TODO use sorted data structure
                var searchToExpand = caveSearchesToExpand.MinBy(searchComparer);

                var expandedSearches = ExpandCaveSearch(searchToExpand, cave)
                    .Where(s => !reachedSearches.TryGetValue(s, out var other) || s.Cost < other.Cost);

                caveSearchesToExpand.Remove(searchToExpand);

                foreach (var expandedSearch in expandedSearches)
                {
                    caveSearchesToExpand.AddOrUpdate(expandedSearch, (@new, old) => @new.Cost < old.Cost);
                    reachedSearches.AddOrUpdate(expandedSearch, (@new, old) => @new.Cost < old.Cost);
                }

                reachedSearches.TryGetValue(targetSearch, out currentSearchToTarget);

            } while (currentSearchToTarget == null);

            return currentSearchToTarget;
        }

        private IEnumerable<CaveSearch> ExpandCaveSearch(CaveSearch caveSearchToExpand, Cave cave)
        {
            return GetPossibleActions(caveSearchToExpand, cave)
                .Select(a => a.ApplyAction(caveSearchToExpand));
        }

        private IEnumerable<IAction> GetPossibleActions(CaveSearch caveSearch, Cave cave)
        {
            if(cave.TargetLocation == caveSearch.Location)
                return new ChangeEquipment(Torch.Value).RepeatOnce();

            return MoveDirections
                .Select(d => (destination: caveSearch.Location + d, direction: d))
                .Where(m => m.destination.X >= 0 && m.destination.Y >= 0)
                .Select(m =>
                {
                    var regionAtDestination = cave[m.destination];
                    var regionAtCurrent = cave[caveSearch.Location];

                    if (caveSearch.Equipment.CanEnter(regionAtDestination))
                        return new MoveInDirection(m.direction) as IAction;

                    return Equipments
                        .Where(e => CanEnterRegionsWithEquipment(e, regionAtCurrent, regionAtDestination))
                        .Select(e => new ChangeEquipment(e))
                        .First();
                });
        }

        private bool CanEnterRegionsWithEquipment(IEquipment equipment, params CaveRegion[] regions)
        {
            return regions.All(r => equipment.CanEnter(r));
        }
    }
}
