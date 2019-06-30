using AdventCodeSolution.Day3;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day17
{
    public class WaterStream
    {
        private readonly XY sourceLocation;
        private readonly ClayMap clayMap;

        public WaterStream(XY sourceLocation, ClayMap clayMap)
        {
            this.sourceLocation = sourceLocation;
            this.clayMap = clayMap;
        }

        public IDictionary<XY, Water> GetStream()
        {
            var streamSource = Water.CreateSourceAt(sourceLocation);
            ICollection<Water> waterNodesToSpread = new List<Water>() { streamSource };
            var existingWater = streamSource.RepeatOnce().ToDictionary(w => w.Location, w => w);

            do
            {
                var newWater = new Dictionary<XY, Water>();

                foreach (var water in waterNodesToSpread)
                {
                    var waterToAdd = GetFlowDirections(water, existingWater)
                        .Select(d => water.FlowToDirection(d))
                        .Where(w => !clayMap.IsBelowMap(w.Location))
                        .ToArray();

                    existingWater.AddRange(waterToAdd, w => w.Location, w => w);
                    newWater.AddRange(waterToAdd, w => w.Location, w => w);

                    if (waterToAdd.Length == 0 && IsWaterLevelStable(water, existingWater))
                    {
                        var waterInASameLevel = EnumerateWaterInSameLavel(water.Location, existingWater).ToArray();

                        var sourcesToAdd = waterInASameLevel
                            .Where(w => existingWater.ContainsKey(w.Location + XY.Up))
                            .Select(w => existingWater[w.Location + XY.Up])
                            .ToArray();

                        newWater.AddOrUpdateRange(sourcesToAdd, w => w.Location, w => w, (@new, old) => @new);

                        foreach (var w in waterInASameLevel)
                            w.Stabilize();
                    }
                }

                waterNodesToSpread = newWater.Values;

            } while (waterNodesToSpread.Count > 0);

            return existingWater;
        }

        private XY[] GetFlowDirections(Water water, IDictionary<XY, Water> existingWater)
        {
            var locationBelow = water.Location + XY.Down;

            if (IsLocationEmpty(locationBelow, existingWater))
                return XY.Down.RepeatOnce().ToArray();

            var isWaterBelow = existingWater.TryGetValue(locationBelow, out var waterBelow);

            if (!isWaterBelow || waterBelow.IsStable)
            {
                return GetPossibleFlowDirections()
                    .Where(d => IsLocationEmpty(water.Location + d, existingWater))
                    .ToArray();
            }

            return new XY[0];
        }

        private bool IsWaterLevelStable(Water water, IDictionary<XY, Water> existingWater)
        {
            var waterInASameLevel = EnumerateWaterInSameLavel(water.Location, existingWater).ToArray();

            if (waterInASameLevel.All(w => w.IsStable))
                return true;

            var waterCanFlowFurther = waterInASameLevel.Any(w => HasEmptySpaceAround(w, existingWater));

            if (waterCanFlowFurther)
                return false;
              
            var waterInLowerLevel = waterInASameLevel.SelectMany(w => w.FurtherFlows).Where(w => w.Location.Y < water.Location.Y).ToArray();

            if (waterInLowerLevel.Length == 0)
                return true;

            return waterInLowerLevel.All(w => IsWaterLevelStable(w, existingWater));
        }

        private IEnumerable<Water> EnumerateWaterInSameLavel(XY start, IDictionary<XY, Water> existingWaters)
        {
            var waterInLeft = EnumerateWaterInDirection(start, XY.Left, existingWaters);
            var waterInRight = EnumerateWaterInDirection(start + XY.Right, XY.Right, existingWaters);

            return waterInLeft.Concat(waterInRight);
        }

        private IEnumerable<Water> EnumerateWaterInDirection(XY location, XY direction, IDictionary<XY, Water> existingWaters)
        {
            var currentLocation = location;

            while(existingWaters.TryGetValue(currentLocation, out var water))
            {
                yield return water;
                currentLocation += direction;
            }
        }

        private bool HasEmptySpaceAround(Water water, IDictionary<XY, Water> existingWaters)
        {
            return GetPossibleFlowDirections()
                .Select(d => water.Location + d)
                .Any(l => IsLocationEmpty(l, existingWaters));
        }

        private bool IsLocationEmpty(XY location, IDictionary<XY, Water> existingWaters)
        {
            return !clayMap.IsClayAtLocation(location) && !existingWaters.ContainsKey(location);
        }

        public static IEnumerable<XY> GetPossibleFlowDirections()
        {
            yield return XY.Down;
            yield return XY.Left;
            yield return XY.Right;
        }
    }
}
