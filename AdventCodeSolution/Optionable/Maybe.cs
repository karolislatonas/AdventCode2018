using System;

namespace AdventCodeSolution.Optionable
{
    public abstract class Maybe<T>
    {
        public T Reduce(T whenNone) => Reduce(() => whenNone);

        public abstract T Reduce(Func<T> whenNone);

        public abstract Maybe<TMap> Map<TMap>(Func<T, TMap> map);

        public abstract Maybe<T> WhenSome(Action<T> action);

        public abstract Maybe<T> WhenNone(Action action);

        public static implicit operator Maybe<T>(None none) => new None<T>();
    }

    public static class Maybe
    {
        public static Maybe<T> Some<T>(T value) => new Some<T>(value);

        public static Maybe<T> NoneIfNull<T>(T value) => ReferenceEquals(value, null) ? None.Value : Some(value);
    }
}
