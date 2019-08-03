using AdventCodeSolution.Day03;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day06
{
    public class Area
    {
        public Area(XY location, IEnumerable<XY> areaPoints)
        {
            Location = location;
            AreaPoints = areaPoints.ToArray();
        }

        public XY Location { get; }

        public IReadOnlyList<XY> AreaPoints { get; }

        public int Size => AreaPoints.Count;

        public bool IsFiniteOnArea(XY topLeft, XY bottomRight)
        {
            bool EqualToAnyBy(XY point, Func<XY, int> getCoordinate) => 
                getCoordinate(point) == getCoordinate(topLeft) || 
                getCoordinate(point) == getCoordinate(bottomRight);

            var result = !AreaPoints.Any(p => EqualToAnyBy(p, c => c.X) || EqualToAnyBy(p, c => c.Y));

            return result;
        }
    }
}
