using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            return sequence.Aggregate((max, c) => getComparable(max).CompareTo(getComparable(c)) < 0 ? c : max);
        }

        public static T MaxBy<T, TComparable>(this IEnumerable<T> sequence, Func<T, TComparable> getComparable, IComparer<TComparable> comparer)
        {
            return sequence.Aggregate((max, c) => comparer.Compare(getComparable(max), getComparable(c)) < 0 ? c : max);
        }

        public static T MinBy<T, TComparable>(this IEnumerable<T> sequence, Func<T, TComparable> getComparable)
            where TComparable : IComparable<TComparable>
        {
            return sequence.Aggregate((min, c) => getComparable(min).CompareTo(getComparable(c)) > 0 ? c : min);
        }

        public static T MinBy<T, TComparable>(this IEnumerable<T> sequence, Func<T, TComparable> getComparable, IComparer<TComparable> comparer)
        {
            return sequence.Aggregate((min, c) => comparer.Compare(getComparable(min), getComparable(c)) > 0 ? c : min);
        }

        public static T[] MultipleMinBy<T, TComparable>(this IEnumerable<T> sequence, Func<T, TComparable> getComparable)
            where TComparable : IComparable<TComparable>
        {
            return sequence
                .Aggregate(new List<T>(), (mins, c) => {
                    if(mins.Count == 0)
                    {
                        mins.Add(c);
                        return mins;
                    }

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

        public static Dictionary<TKey, TValue> ToDictionary<T, TKey, TValue>(this IEnumerable<T> sequence, 
            Func<T, TKey> getKey, 
            Func<T, TValue> create, 
            Func<TValue, TValue> update)
        {
            return sequence.ToDictionary(getKey, create, (i, old) => update(old));
        }

        public static Dictionary<TKey, TValue> ToDictionary<T, TKey, TValue>(this IEnumerable<T> sequence,
            Func<T, TKey> getKey,
            Func<T, TValue> create,
            Func<T, TValue, TValue> update)
        {
            var result = new Dictionary<TKey, TValue>();

            foreach (var item in sequence)
            {
                var key = getKey(item);

                var newValue = result.TryGetValue(key, out var currentValue) ?
                    update(item, currentValue) :
                    create(item);

                result[key] = newValue;
            }

            return result;
        }

        public static SortedDictionary<TKey, TValue> ToSortedDictionary<T, TKey, TValue>(
            this IEnumerable<T> sequence, Func<T, TKey> getKey, Func<T, TValue> getValue, IComparer<TKey> comparer)
        {
            var sortedDictionary = new SortedDictionary<TKey, TValue>(comparer);

            foreach(var item in sequence)
            {
                sortedDictionary.Add(getKey(item), getValue(item));
            }

            return sortedDictionary;
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

        public static void AddOrUpdateRange<TItem, TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TItem> items, Func<TItem, TKey> getKey, Func<TItem, TValue> create, Func<TItem, TValue, TValue> update)
        {
            foreach (var item in items)
            {
                var key = getKey(item);

                dictionary.AddOrUpdate(key, k => create(item), (k, v) => update(item, v));
            }
        }

        public static void AddRange<TValue>(this HashSet<TValue> set, IEnumerable<TValue> values)
        {
            foreach (var value in values)
                set.Add(value);
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

        public static IEnumerable<int> EnumerateFrom(this int start)
        {
            var current = start;

            do
            {
                yield return current;
                current++;
            } while (true);
        }

        public static bool IsIndexInRange<T>(this IList<T> list, int i) => 0 <= i && i < list.Count;

        public static IEnumerable<T> ReverseEnumerate<T>(this IReadOnlyList<T> list)
        {
            for(var i = list.Count - 1; i >= 0; i--)
            {
                yield return list[i];
            }
        }

        public static IEnumerable<T> TakeFrom<T>(this IReadOnlyList<T> list, int indexFrom, int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return list[indexFrom + i];
            }
        }

        public static string JoinIntoString<T>(this IEnumerable<T> numbers)
        {
            return numbers.Aggregate(new StringBuilder(), (builder, i) => builder.Append(i)).ToString();
        }

        public static void AddRange<TItem, TKey, TValue>(this IDictionary<TKey, TValue> set, IEnumerable<TItem> items, Func<TItem, TKey> getKey, Func<TItem, TValue> getValue)
        {
            foreach (var item in items)
            {
                set.Add(getKey(item), getValue(item));
            }   
        }
    }
}
