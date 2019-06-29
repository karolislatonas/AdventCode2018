using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution
{
    public static class ObjectExtensions
    {
        public static TNew Map<T, TNew>(this T obj, Func<T, TNew> map) => map(obj);

        public static IEnumerable<T> RepeatOnce<T>(this T obj) => Enumerable.Repeat(obj, 1);

        public static IEnumerable<T> StartEnumerate<T>(this T initialValue, Func<T, T> getNext)
        {
            var currentValue = initialValue;

            do
            {
                yield return currentValue;

                currentValue = getNext(currentValue);
            } while (true);
        }
    }
}
