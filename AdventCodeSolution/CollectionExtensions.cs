using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> Infinitely<T>(this IEnumerable<T> sequence)
        {
            while (true)
            {
                foreach (var item in sequence)
                    yield return item;
            }
        }

        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> sequence) => sequence.Where(i => !(i is null));

        public static IEnumerable<string> WhereNotNullOrEmpty(this IEnumerable<string> sequence) => sequence.Where(s => !string.IsNullOrEmpty(s));

        public static T MaxBy<T, TComparable>(this IEnumerable<T> sequence, Func<T, TComparable> getComparable)
            where TComparable : IComparable<TComparable>
        {
            return sequence.Aggregate((max, c) => getComparable(max).CompareTo(getComparable(c)) <= 0 ? c : max);
        }

        public static T MinBy<T, TComparable>(this IEnumerable<T> sequence, Func<T, TComparable> getComparable)
            where TComparable : IComparable<TComparable>
        {
            return sequence.Aggregate((min, c) => getComparable(min).CompareTo(getComparable(c)) <= 0 ? min : c);
        }

        public static T[] MultipleMinBy<T, TComparable>(this IEnumerable<T> sequence, Func<T, TComparable> getComparable)
            where TComparable : IComparable<TComparable>
        {
            var minValues = new List<T>() { sequence.First() };

            return sequence
                .Skip(1)
                .Aggregate(minValues, (mins, c) => {
                    var currentMin = mins[0];
                    var comparisonResult = getComparable(currentMin).CompareTo(getComparable(c));

                    if(comparisonResult > 0) return new List<T>() { c };

                    if (comparisonResult == 0) mins.Add(c);

                    return mins;
                })
                .ToArray();
        }

        public static TResult AggregateOrDefault<TElement, TResult>(this IEnumerable<TElement> sequence, Func<IEnumerable<TElement>, TResult> aggregate, TResult defaultValue)
        {
            return sequence.Any() ? aggregate(sequence) : defaultValue;
        }

        public static Dictionary<TKey, TElement> ToDictionary<T, TKey, TElement>(this IEnumerable<T> sequence, 
            Func<T, TKey> getKey, 
            Func<T, TElement> create, 
            Func<TElement, TElement> update)
        {
            var result = new Dictionary<TKey, TElement>();

            foreach (var item in sequence)
            {
                var key = getKey(item);

                var newValue = result.TryGetValue(key, out var currentValue) ?
                    update(currentValue) :
                    create(item);

                result[key] = currentValue;
            }

            return result;
        }

        public static List<TElement> RemoveWhere<TElement>(this List<TElement> sequence, Func<TElement, bool> shouldRemove)
        {
            var removedItems = new List<TElement>();

            for(var i = 0; i < sequence.Count; i++)
            {
                if (shouldRemove(sequence[i]))
                {
                    removedItems.Add(sequence[i]);
                    sequence.RemoveAt(i);
                    i--;
                }
            }

            return removedItems;
        }

        public static KeyValuePair<TKey, TElement> MaxByValue<TKey, TElement>(this IDictionary<TKey, TElement> dictionary)
            where TElement : IComparable<TElement>
        {
            return dictionary.MaxBy(kvp => kvp.Value);
        }

        public static IEnumerable<TElement> Slice<TElement>(this IList<TElement> list, int startIndex, int count)
        {
            for(var i = 0; i < count; i++)
            {
                yield return list[startIndex + i];
            }
        }

        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> create, Func<TKey, TValue, TValue> update)
        {
            var newValue = dictionary.TryGetValue(key, out var value) ?
                update(key, value) :
                create(key);

            dictionary[key] = newValue;
        }

        public static IEnumerable<LinkedListNode<TElement>> EnumerateNodes<TElement>(this LinkedList<TElement> linkedList)
        {
            var current = linkedList.First;
            while (current != null)
            {
                yield return current;
                current = current.Next;
            }
        }

        public static LinkedList<TElement> ToLinkedList<TElement>(this IEnumerable<TElement> sequence) => new LinkedList<TElement>(sequence);

        public static IEnumerable<LinkedListNode<TElement>> ReverseEnumerateInfinitelyFrom<TElement>(this LinkedList<TElement> linkedList, LinkedListNode<TElement> enumerateFrom)
        {
            var current = enumerateFrom;

            while (true)
            {
                yield return current;
                current = current.Previous ?? linkedList.Last;
            } 
        }
    }
}
