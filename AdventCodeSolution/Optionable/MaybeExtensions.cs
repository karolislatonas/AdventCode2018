using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Optionable
{
    public static class MaybeExtensions
    {
        public static Maybe<T> Some<T>(this T obj) => Maybe.Some(obj);

        public static Maybe<T> FirstOrNone<T>(this IEnumerable<T> sequence)
            where T : class
        {
            return sequence
                .Select(Maybe.Some)
                .DefaultIfEmpty(None.Value)
                .First();
        }

        public static Maybe<T> FirstOrNone<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
            where T : class
        {
            return FirstOrNone(sequence.Where(predicate));
        }

        public static Maybe<TMap> NoneIfEmpty<T, TMap>(this IEnumerable<T> sequence, Func<IEnumerable<T>, TMap> selector)
            where T : class
        {
            return sequence.Any() ? selector(sequence).Some() : None.Value;
        }
    }
}
