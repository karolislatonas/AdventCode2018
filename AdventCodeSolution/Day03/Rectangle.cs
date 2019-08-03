using System.Collections.Generic;
using System.Diagnostics;
using static System.Math;

namespace AdventCodeSolution.Day03
{
    [DebuggerDisplay("Location: {Location}, Size: {Size}")]
    public class Rectangle : ValueObject<Rectangle>
    {
        public Rectangle(XY location, XY size)
        {
            Location = location;
            Size = size;
        }

        public XY Location { get; }

        public XY Size { get; }

        public XY BottomRight => Location + Size;

        public int Square => Size.X * Size.Y;

        public Rectangle GetOverlap(Rectangle other)
        {
            var location = new XY(Max(Location.X, other.Location.X), Max(Location.Y, other.Location.Y));
            var bottomRight = new XY(Min(BottomRight.X, other.BottomRight.X), Min(BottomRight.Y, other.BottomRight.Y));

            var overlaps = location.X < bottomRight.X && location.Y < bottomRight.Y;

            return overlaps ?
                new Rectangle(location, bottomRight - location) :
                null;
        }

        public IEnumerable<XY> EnumeratePoints()
        {
            for(var i = 0; i < Size.X; i++)
                for (var j = 0; j < Size.Y; j++)
                {
                    yield return new XY(Location.X + i, Location.Y + j);
                }
        }

        protected override bool EqualsCore(Rectangle other)
        {
            throw new System.NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new System.NotImplementedException();
        }
    }
}
