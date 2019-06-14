using System.Diagnostics;
using static System.Math;

namespace AdventCodeSolution.Day3
{
    [DebuggerDisplay("X: {X}, Y: {Y}")]
    public class XY : ValueObject<XY>
    {
        public XY(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public static XY Zero { get; } = new XY(0, 0);

        public static XY Right { get; } = new XY(1, 0);

        public static XY Up { get; } = new XY(0, 1);

        public static XY Left { get; } = -Right;

        public static XY Down { get; } = -Up;

        public static XY operator +(XY left, XY right) => new XY(left.X + right.X, left.Y + right.Y);

        public static XY operator -(XY left, XY right) => new XY(left.X - right.X, left.Y - right.Y);

        public static XY operator *(XY left, int scale) => new XY(left.X * scale, left.Y * scale);

        public static XY operator -(XY xy) => new XY(-xy.X, -xy.Y);

        public int GetManhattanDistance(XY other)
        {
            return Abs(X - other.X) + Abs(Y - other.Y);
        }

        protected override int GetHashCodeCore() => 17 ^ X ^ Y;

        protected override bool EqualsCore(XY other)
        {
            return X == other.X &&
                Y == other.Y;
        }

        public override string ToString() => $"{X},{Y}";

        public static XY Parse(string input, char seperator)
        {
            var coordinates = input.Split(seperator);

            int Parse(string coordinate) => int.Parse(coordinate.Trim());

            return new XY(Parse(coordinates[0]), Parse(coordinates[1]));
        }
    }
}
