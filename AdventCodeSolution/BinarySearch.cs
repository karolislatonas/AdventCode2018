using System;

namespace AdventCodeSolution
{
    public class BinarySearch
    {
        public static int ForawrdSearch(int start, Func<int, int> getNext, Func<int, int> getDirection)
        {
            var left = start;
            var right = start;
            var current = start;

            while (getDirection(current) > 0)
            {
                left = right;

                current = getNext(current);

                right = current;
            }

            int previousCurrent;
            do
            {
                previousCurrent = current;
                current = (left + right + 1) / 2; // +1 to take higher
                var direction = getDirection(current);

                if (direction < 0)
                    right = current;
                else if (direction > 0)
                    left = current;
                else
                {
                    left = current;
                    right = current;
                }
            } while (previousCurrent != current);

            return current;
        }

        private class LeftRight
        {
            public LeftRight(int left, int right)
            {
                Left = left;
                Right = right;
            }

            public int Left { get; }

            public int Right { get; }

            public LeftRight ExpandToRight(int right)
            {
                return new LeftRight(Right, right);
            }

            public LeftRight ExpandToLeft(int left)
            {
                return new LeftRight(left, Left);
            }

            public int GetLowerMiddle() => (Left + Right) / 2;

            public int GetHigherMiddle() => (Left + Right + 1) / 2;

            public LeftRight MakeSmallerInDirection(int direction)
            {
                var middle = GetLowerMiddle();

                return direction < 0 ? new LeftRight(Left, middle) :
                       direction > 0 ? new LeftRight(middle, Right) :
                                       new LeftRight(middle, middle);
            }
        }
    }
}
