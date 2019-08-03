using AdventCodeSolution.Day10;
using AdventCodeSolution.Day18.Acres;
using AdventCodeSolution.Day03;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventCodeSolution.Day18
{
    public class MagicArea : ValueObject<MagicArea>
    {
        private readonly IReadOnlyDictionary<XY, Acre> acres;
        private readonly Lazy<int> lumberyardsCountLazy;
        private readonly Lazy<int> treesCountLazy;
        private readonly Lazy<int> hashCodeLazy;

        public MagicArea(IEnumerable<Acre> acres)
        {
            this.acres = acres.ToDictionary(a => a.Location, a => a);

            lumberyardsCountLazy = new Lazy<int>(() => this.acres.Values.OfType<Lumberyard>().Count());
            treesCountLazy = new Lazy<int>(() => this.acres.Values.OfType<Tree>().Count());
            hashCodeLazy = new Lazy<int>(() => this.acres.Values.Aggregate(17, (t, c) => t ^ c.GetHashCode()));
        }

        public int LumberyardsCount => lumberyardsCountLazy.Value;

        public int TreesCount => treesCountLazy.Value;

        public MagicArea GetMagicAreaAfterMinutes(int minutes)
        {
            var currentMinute = 0;
            var currentArea = this;

            var occuredPreviously = false;
            var previousAreas = new Dictionary<MagicArea, int>() { [currentArea] = 0 };

            while (currentMinute < minutes && !occuredPreviously)
            {
                currentMinute++;
                currentArea = currentArea.Transform();

                occuredPreviously = !previousAreas.TryAdd(currentArea, currentMinute);
            }

            if (currentMinute == minutes)
                return currentArea;

            var firstTimeOccuredInMinute = previousAreas[currentArea];

            var reoccuredAfterMinutes = currentMinute - firstTimeOccuredInMinute;
            var minutesLeft = (minutes - currentMinute) % reoccuredAfterMinutes;

            return currentArea.GetMagicAreaAfterMinutes(minutesLeft);
        }

        private MagicArea Transform()
        {
            var transformedAcres = acres
                .Values
                .AsParallel()
                .Select(a => a.Transform(GetAdjacentAcre(a.Location)));

            return new MagicArea(transformedAcres);
        }

        private IEnumerable<Acre> GetAdjacentAcre(XY location)
        {
            return AdjacentAcreLocations(location)
                .Where(l => acres.ContainsKey(l))
                .Select(l => acres[l]);
        }

        private static IEnumerable<XY> AdjacentAcreLocations(XY location)
        {
            yield return location + XY.Up;
            yield return location + XY.Down;
            yield return location + XY.Left;
            yield return location + XY.Right;

            yield return location + XY.Up + XY.Left;
            yield return location + XY.Up + XY.Right;
            yield return location + XY.Down + XY.Left;
            yield return location + XY.Down + XY.Right;
        }

        private bool ContainsAcre(Acre acre)
        {
            return acres.TryGetValue(acre.Location, out var otherAcre) && acre == otherAcre;
        }

        protected override int GetHashCodeCore() => hashCodeLazy.Value;

        protected override bool EqualsCore(MagicArea other)
        {
            return acres.Count == other.acres.Count &&
                acres.Values.All(other.ContainsAcre);
        }

        private string CreateStringMap()
        {
            var bottomRight = acres.Values.MaxBy(a => a.Location, FromLeftHighestToRightLowestComparer.Value).Location;

            var builder = new StringBuilder();
            for (var y = 0; y >= bottomRight.Y; y--)
            {
                for (var x = 0; x <= bottomRight.X; x++)
                {
                    var a = acres[new XY(x, y)];
                    builder.Append(a.Symbol);
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}
