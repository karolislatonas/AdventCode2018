using static System.Math;

namespace AdventCodeSolution.Day23
{
    public sealed class XYZ : ValueObject<XYZ>
    {
        public XYZ(int xyz) : 
            this(xyz, xyz, xyz)
        {
            
        }

        public XYZ(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; }

        public int Y { get; }

        public int Z { get; }

        public double Length => Sqrt(X * X + Y * Y + Z * Z);

        public XYZ XDirection => new XYZ(Sign(X), 0, 0);

        public XYZ YDirection => new XYZ(0, Sign(Y), 0);

        public XYZ ZDirection => new XYZ(0, 0, Sign(Z));

        public static XYZ Zero { get; } = new XYZ(0, 0, 0);

        public static XYZ XAxis { get; } = new XYZ(1, 0, 0);

        public static XYZ YAxis { get; } = new XYZ(0, 1, 0);

        public static XYZ ZAxis { get; } = new XYZ(0, 0, 1);

        public static XYZ operator +(XYZ left, XYZ right) => new XYZ(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        public static XYZ operator -(XYZ left, XYZ right) => new XYZ(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        public static XYZ operator *(XYZ left, int scale) => new XYZ(left.X * scale, left.Y * scale, left.Z * scale);

        public static XYZ operator -(XYZ xyz) => new XYZ(-xyz.X, -xyz.Y, -xyz.Z);

        public int GetManhattanDistance(XYZ other)
        {
            return Abs(X - other.X) + Abs(Y - other.Y) + Abs(Z - other.Z);
        }

        protected override int GetHashCodeCore() => 17 ^ X ^ Y ^ Z;

        protected override bool EqualsCore(XYZ other)
        {
            return X == other.X &&
                Y == other.Y &&
                Z == other.Z;
        }

        public override string ToString() => $"{X},{Y},{Z}";

        public static XYZ Parse(string input, char seperator)
        {
            var coordinates = input.Split(seperator);

            int Parse(string coordinate) => int.Parse(coordinate.Trim());

            return new XYZ(Parse(coordinates[0]), Parse(coordinates[1]), Parse(coordinates[2]));
        }
    }
}
