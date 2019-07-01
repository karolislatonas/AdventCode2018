using AdventCodeSolution.Day3;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day17
{
    public class WaterStream : IEnumerable<Water>
    {
        private readonly Dictionary<XY, Water> waterLocations;

        public WaterStream(XY initialSourceLocation) : 
            this(Water.CreateSourceAt(initialSourceLocation))
        {
            
        }

        public WaterStream(Water initialSource)
        {
            InitialSource = initialSource;

            waterLocations = new Dictionary<XY, Water>()
            {
                [initialSource.Location] = initialSource
            };
        }

        public Water InitialSource { get; }

        public int CountStableWater() => waterLocations.Values.Count(w => w.IsStable);

        public Water[] SpreadStreamAtLocation(XY location, ClayMap clayMap)
        {
            var water = GetWater(location);

            var directionsToFlow = GetFlowDirections(water.Location, clayMap);

            var newAddedWater = new Water[directionsToFlow.Length];
            for (var i = 0; i < directionsToFlow.Length; i++)
            {
                var newWater = water.FlowToDirection(directionsToFlow[i]);
                newAddedWater[i] = newWater;
                AddWater(newWater);
            }

            return newAddedWater;
        }

        public bool HasWaterAtLocation(XY location) => waterLocations.ContainsKey(location);

        public Water GetWater(XY location) => waterLocations[location];

        public bool TryGetWater(XY location, out Water water) => waterLocations.TryGetValue(location, out water);

        private void AddWater(Water waterToAdd)
        {
            waterLocations.Add(waterToAdd.Location, waterToAdd);
        }

        public void StabilizeWaterInSameLevel(XY location)
        {
            var waterInASameLevel = EnumerateWaterInSameLavel(location);

            foreach (var w in waterInASameLevel)
                w.Stabilize();
        }

        public Water[] GetUpperLevelSourceWater(XY location)
        {
            return EnumerateWaterInSameLavel(location)
                .Where(w => HasWaterAtLocation(w.Location + XY.Up))
                .Select(w => GetWater(w.Location + XY.Up))
                .ToArray();
        }

        public IEnumerator<Water> GetEnumerator() => waterLocations.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerable<Water> EnumerateWaterInSameLavel(XY start)
        {
            var waterInLeft = EnumerateWaterInDirection(start, XY.Left);
            var waterInRight = EnumerateWaterInDirection(start + XY.Right, XY.Right);

            return waterInLeft.Concat(waterInRight);
        }

        private IEnumerable<Water> EnumerateWaterInDirection(XY location, XY direction)
        {
            var currentLocation = location;

            while (waterLocations.TryGetValue(currentLocation, out var water))
            {
                yield return water;
                currentLocation += direction;
            }
        }

        private XY[] GetFlowDirections(XY location, ClayMap clayMap)
        {
            bool CanFlowToLocation(XY loc) => !clayMap.IsClayAtLocation(loc) && !HasWaterAtLocation(loc);

            var locationBelow = location + XY.Down;

            if (CanFlowToLocation(locationBelow))
                return XY.Down.RepeatOnce().ToArray();

            var isWaterBelow = TryGetWater(locationBelow, out var waterBelow);

            if (!isWaterBelow || waterBelow.IsStable)
            {
                return Water.GetPossibleFlowDirections()
                    .Where(d => CanFlowToLocation(location + d))
                    .ToArray();
            }

            return new XY[0];
        }
    }
}
