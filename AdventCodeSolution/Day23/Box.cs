using System;

namespace AdventCodeSolution.Day23
{
    public class Box : ValueObject<Box>
    {
        public Box(XYZ startPoint, XYZ endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public XYZ StartPoint { get; }

        public XYZ EndPoint { get; }

        public Box GetIntersection(Box other)
        {
            (int start, int end) GetStartEndOfAxis(Func<XYZ, int> getAxisValue)
            {
                return (start: Math.Max(getAxisValue(StartPoint), getAxisValue(other.StartPoint)),
                        end: Math.Min(getAxisValue(EndPoint), getAxisValue(other.EndPoint)));
            }

            var (startX, endX) = GetStartEndOfAxis(p => p.X);
            var (startY, endY) = GetStartEndOfAxis(p => p.Y);
            var (startZ, endZ) = GetStartEndOfAxis(p => p.Z);

            var isIntersecting = endX - startX >= 0 &&
                                 endY - startY >= 0 &&
                                 endZ - startZ >= 0;

            return isIntersecting ?
                new Box(new XYZ(startX, startY, startZ), new XYZ(endX, endY, endZ))
                
                :
                null;
        }

        protected override bool EqualsCore(Box other)
        {
            return StartPoint == other.StartPoint &&
                   EndPoint == other.EndPoint;
        }

        protected override int GetHashCodeCore()
        {
            return StartPoint.GetHashCode() ^ EndPoint.GetHashCode();
        }
    }
}
