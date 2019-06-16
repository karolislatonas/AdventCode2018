using System;
using System.Collections.Generic;
using System.Text;

namespace AdventCodeSolution.Optionable
{
    public class None<T> : Maybe<T>
    {
        public override Maybe<TMap> Map<TMap>(Func<T, TMap> map) => None.Value;

        public override T Reduce(Func<T> whenNone) => whenNone();

        public override Maybe<T> WhenNone(Action action)
        {
            action();
            return this;
        }

        public override Maybe<T> WhenSome(Action<T> action)
        {
            return this;
        }
    }

    public class None
    {
        private None()
        {

        }

        public static None Value { get; } = new None();
    }
}
