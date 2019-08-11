using AdventCodeSolution.Day03;
using AdventCodeSolution.Day22.Equipments;
using AdventCodeSolution.Day22.Regions;
using AdventCodeSolution.Day22.SearchActions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day22
{
    public class PathFinder
    {
        public CaveSearch FindQuickestPath(Cave cave)
        {
            var targetSearch = new CaveSearch(cave.TargetLocation, Torch.Value);
            var initialSearch = new CaveSearch(cave.InitialLocation, Torch.Value);
            var caveSearchesToExpand = initialSearch.RepeatOnce().ToHashSet(SearchEqualityComparerByEquipmentAndLocation.Value);
            var reachedSearches = initialSearch.RepeatOnce().ToHashSet(SearchEqualityComparerByEquipmentAndLocation.Value);

            CaveSearch currentSearchToTarget = null;

            do
            {
                var tempSaveSearchesToExpand = caveSearchesToExpand;
                var expandedSearches = tempSaveSearchesToExpand
                    .SelectMany(s => ExpandCaveSearch(s, cave))
                    .Where(s => !reachedSearches.TryGetValue(s, out var other) || s.Cost < other.Cost)
                    .Where(s => currentSearchToTarget == null || s.Cost < currentSearchToTarget.Cost);

                caveSearchesToExpand = new HashSet<CaveSearch>(SearchEqualityComparerByEquipmentAndLocation.Value);

                foreach (var expandedSearch in expandedSearches)
                {
                    caveSearchesToExpand.AddOrUpdate(expandedSearch, (@new, old) => @new.Cost < old.Cost);
                    reachedSearches.AddOrUpdate(expandedSearch, (@new, old) => @new.Cost < old.Cost);
                }

                reachedSearches.TryGetValue(targetSearch, out currentSearchToTarget);

            } while (caveSearchesToExpand.Count > 0);

            return currentSearchToTarget;
        }

        private int CalculateMaxCost(int totalDistanceLeft)
        {
            return totalDistanceLeft * 8 - 1;
        }

        private int CalculateMinCost(int totalDistanceLeft)
        {
            return totalDistanceLeft - 1;
        }

        private IEnumerable<CaveSearch> ExpandCaveSearch(CaveSearch caveSearchToExpand, Cave cave)
        {
            return GetPossibleActions(caveSearchToExpand, cave)
                .Select(a => a.ApplyAction(caveSearchToExpand));
        }

        private IEnumerable<IAction> GetPossibleActions(CaveSearch caveSearch, Cave cave)
        {
            if(cave.TargetLocation == caveSearch.Location)
            {
                return new ChangeEquipment(Torch.Value).RepeatOnce();
            }

            return EnumerateMovementDirections()
                .Select(d => (destination: caveSearch.Location + d, direction: d))
                .Where(m => m.destination.X >= 0 && m.destination.Y >= 0)
                .Select(m =>
                {
                    var regionAtDestination = cave[m.destination];
                    if (caveSearch.Equipment.CanEnter(regionAtDestination))
                        return new MoveInDirection(m.direction);

                    var regionAtCurrent = cave[caveSearch.Location];

                    var equipment = EnumerateEquipments()
                        .First(e => CanEnterRegionsWithEquipment(e, regionAtCurrent, regionAtDestination));

                    return new ChangeEquipment(equipment) as IAction;
                });
        }

        private bool CanEnterRegionsWithEquipment(IEquipment equipment, params CaveRegion[] regions)
        {
            return regions.All(r => equipment.CanEnter(r));
        }

        private IEnumerable<XY> EnumerateMovementDirections()
        {
            yield return XY.Up;
            yield return XY.Left;
            yield return XY.Right;
            yield return XY.Down;
        }

        private IEnumerable<IEquipment> EnumerateEquipments()
        {
            yield return ClimbingGear.Value;
            yield return NoneEquipment.Value;
            yield return Torch.Value;
        }
    }
}
