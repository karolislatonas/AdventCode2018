using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace AdventCodeSolution.Day25
{
    public class NPoint : ValueObject<NPoint>
    {
        private readonly IReadOnlyList<int> values;

        public NPoint(int dimension) :
            this(new int[dimension])
        {
            
        }

        private NPoint(params int[] values)
        {
            this.values = values;
        }

        public NPoint(IEnumerable<int> dimension)
        {
            values = dimension.ToArray();
        }

        public int Dimension => values.Count;

        public int this[int index] => values[index];

        public double Length => Sqrt(values.Sum(v => v * v));

        public static NPoint Zero(int dimension) => new NPoint(dimension);

        public static NPoint operator +(NPoint left, NPoint right) => new NPoint(ZipWithZeros(left, right, (v1, v2) => v1 + v2));

        public static NPoint operator -(NPoint left, NPoint right) => new NPoint(ZipWithZeros(left, right, (v1, v2) => v1 - v2));

        public static NPoint operator *(NPoint left, int scale) => new NPoint(left.values.Select(v => v * scale));

        public static NPoint operator -(NPoint npoint) => npoint * -1;

        public int GetManhattanDistance(NPoint other)
        {
            return ZipWithZeros(this, other, (v1, v2) => Abs(v1 - v2)).Sum();
        }

        protected override int GetHashCodeCore() => values.Aggregate(17, (t, v) => t ^ v);

        protected override bool EqualsCore(NPoint other)
        {
            return ZipWithZeros(this, other, (v1, v2) => v1 == v2).All(v => v);
        }

        public override string ToString() => string.Join(',', values);

        public static NPoint Parse(string input) => Parse(input, ',');

        public static NPoint Parse(string input, char seperator)
        {
            var values = input
                .Split(seperator)
                .Select(v => v.Trim())
                .Select(int.Parse);

            return new NPoint(values);
        }

        private static T[] ZipWithZeros<T>(NPoint p1, NPoint p2, Func<int, int, T> combine)
        {
            var dimension = Max(p1.Dimension, p2.Dimension);

            var transformedValues = new T[dimension];
            for(var i = 0; i < dimension; i++)
            {
                transformedValues[i] = combine(
                    p1.GetValueOrZeroIfOutOfDimension(i), 
                    p2.GetValueOrZeroIfOutOfDimension(i));
            }

            return transformedValues;
        }

        private int GetValueOrZeroIfOutOfDimension(int index)
        {
            return index < Dimension ?
                values[index] :
                0;
        }
    }
}
