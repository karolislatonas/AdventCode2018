using System;

namespace AdventCodeSolution
{
    public abstract class ValueObject<T> : IEquatable<T>
        where T : ValueObject<T>
    {
        public override bool Equals(object obj) => Equals(obj);

        public bool Equals(T other)
        {
            if (other is null) return false;

            return EqualsCore(other);
        }

        public override int GetHashCode() => GetHashCodeCore();

        protected abstract int GetHashCodeCore();

        protected abstract bool EqualsCore(T other);

        public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;

            return left.Equals(right as T);
        }

        public static bool operator !=(ValueObject<T> left, ValueObject<T> right) => !(left == right);
    }
}
