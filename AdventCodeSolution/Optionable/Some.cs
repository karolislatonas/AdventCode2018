using System;

namespace AdventCodeSolution.Optionable
{
    public class Some<T> : Maybe<T>
    {
        private readonly T value;

        public Some(T value)
        {
            this.value = value;
        }

        public override Maybe<TMap> Map<TMap>(Func<T, TMap> map) => Maybe.Some(map(value));

        public override T Reduce(Func<T> whenNone) => value;

        public override Maybe<T> WhenNone(Action action)
        {
            return this;
        }

        public override Maybe<T> WhenSome(Action<T> action)
        {
            action(value);
            return this;
        }
    }
}
