using AdventCodeSolution.Day3;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day17
{
    public class WaterStreamBuilder
    {
        private readonly XY sourceLocation;
        private readonly ClayMap clayMap;

        public WaterStreamBuilder(XY sourceLocation, ClayMap clayMap)
        {
            this.sourceLocation = sourceLocation;
            this.clayMap = clayMap;
        }

        public WaterStream GetStream()
        {
            var stream = new WaterStream(sourceLocation);
            var waterNodesToSpread = stream.Select(w => w.Location).ToHashSet();

            do
            {
                var locationsToContinueSpreadingAt = new HashSet<XY>();

                foreach (var waterLocation in waterNodesToSpread)
                {
                    var spreadedWater = stream.SpreadStreamAtLocation(waterLocation, clayMap);
                    var waterLocationInClayMap = spreadedWater.Select(w => w.Location).Where(l => !clayMap.IsBelowMap(l));

                    locationsToContinueSpreadingAt.AddRange(waterLocationInClayMap);

                    if (spreadedWater.Length == 0 && IsWaterLevelStableAt(waterLocation, stream))
                    {
                        var upperLevelWaterLocations = stream.GetUpperLevelSourceWater(waterLocation).Select(w => w.Location);
                        stream.StabilizeWaterInSameLevel(waterLocation);

                        locationsToContinueSpreadingAt.AddRange(upperLevelWaterLocations);
                    }
                }

                waterNodesToSpread = locationsToContinueSpreadingAt;

            } while (waterNodesToSpread.Count > 0);

            return stream;
        }

        private bool IsWaterLevelStableAt(XY waterLocation, WaterStream stream)
        {
            var waterInASameLevel = stream.EnumerateWaterInSameLavel(waterLocation).ToArray();

            if (waterInASameLevel.Any(w => w.IsStable))
                return true;

            var waterCanFlowFurther = waterInASameLevel.Any(w => HasEmptySpaceAround(w, stream));

            if (waterCanFlowFurther)
                return false;
              
            var waterInLowerLevel = waterInASameLevel
                .SelectMany(w => w.FurtherFlows)
                .Select(w => w.Location)
                .Where(l => l.Y < waterLocation.Y)
                .ToArray();

            if (waterInLowerLevel.Length == 0)
                return true;

            return waterInLowerLevel.All(l => IsWaterLevelStableAt(l, stream));
        }

        private bool HasEmptySpaceAround(Water water, WaterStream stream)
        {
            return Water.GetPossibleFlowDirections()
                .Select(d => water.Location + d)
                .Any(l => IsLocationEmpty(l, stream));
        }

        private bool IsLocationEmpty(XY location, WaterStream stream)
        {
            return !clayMap.IsClayAtLocation(location) && !stream.HasWaterAtLocation(location);
        }
    }
}
