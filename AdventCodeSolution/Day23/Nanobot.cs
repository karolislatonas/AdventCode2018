namespace AdventCodeSolution.Day23
{
    public class Nanobot
    {
        public Nanobot(XYZ location, int signalRadius)
        {
            Location = location;
            SignalRadius = signalRadius;
        }

        public XYZ Location { get; }

        public int SignalRadius { get; }

        public bool IsNanobotInRange(Nanobot other)
        {
            return Location.GetManhattanDistance(other.Location) <= SignalRadius;
        }

        public bool IsNanobotIntersecting(Nanobot other)
        {
            return SignalRadius + other.SignalRadius >= Location.GetManhattanDistance(other.Location);
        }

        public Box GetBoundingBox()
        {
            var radiusSize = new XYZ(SignalRadius);

            return new Box(Location - radiusSize, Location + radiusSize);
        }

        //protected override int GetHashCodeCore()
        //{
        //    return Location.GetHashCode() ^ SignalRadius;
        //}

        //protected override bool EqualsCore(NanoRobot other)
        //{
        //    return Location == other.Location &&
        //        SignalRadius == other.SignalRadius;
        //}
    }
}
