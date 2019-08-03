using AdventCodeSolution.Day03;
using System;

namespace AdventCodeSolution.Day11
{
    public class Square : ValueObject<Square>
    {
        public Square(XY position, int squareSize)
        {
            Position = position;
            Size = squareSize;
        }

        public XY Position { get; }

        public int Size { get; }

        public override string ToString() => $"{Position.X},{Position.Y},{Size}";

        protected override bool EqualsCore(Square other)
        {
            return Position == other.Position &&
                Size == other.Size;
        }

        protected override int GetHashCodeCore()
        {
            return Position.GetHashCode() ^ Size;
        }
    }
}
