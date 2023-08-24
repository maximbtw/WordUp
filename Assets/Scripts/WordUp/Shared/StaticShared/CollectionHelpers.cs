using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordUp.Shared
{
    public static class CollectionHelpers
    {
        public static bool IsNullOrEmpty<T>(ICollection<T> collection) => collection == null || collection.Count == 0;
        
        public static string JoinToString<T>(
            IList<T> collection,
            string separator,
            Func<T, string> converter = null,
            bool sortCollection = false,
            int? maxItems = null,
            string additionalItemsCountFormat = " (+{0})",
            bool ignoreEmptyEntries = true)
        {
            if (IsNullOrEmpty(collection))
            {
                return string.Empty;
            }

            converter = converter ?? (x => x == null ? string.Empty : x.ToString());
            if (collection.Count == 1)
            {
                return converter(collection[index: 0]) ?? string.Empty;
            }

            if (collection.Count == 2)
            {
                string firstString = converter(collection[index: 0]) ?? string.Empty;
                string secondString = converter(collection[index: 1]) ?? string.Empty;
                if (sortCollection)
                {
                    int compareResult = string.Compare(firstString, secondString, StringComparison.OrdinalIgnoreCase);
                    if (compareResult > 0)
                    {
                        (firstString, secondString) = (secondString, firstString);
                    }
                }

                if (ignoreEmptyEntries)
                {
                    if (string.IsNullOrEmpty(firstString))
                    {
                        return secondString;
                    }

                    if (string.IsNullOrEmpty(secondString))
                    {
                        return firstString;
                    }
                }

                if (maxItems == 1)
                {
                    return firstString + string.Format(additionalItemsCountFormat, arg0: 1);
                }

                return firstString + separator + secondString;
            }

            return JoinToString(
                (IEnumerable<T>)collection,
                separator,
                converter,
                sortCollection,
                maxItems,
                additionalItemsCountFormat,
                ignoreEmptyEntries
            );
        }

        static public string JoinToString<T>(
            IEnumerable<T> collection,
            string separator,
            Func<T, string> converter = null,
            bool sortCollection = false,
            int? maxItems = null,
            string additionalItemsCountFormat = " (+{0})",
            bool ignoreEmptyEntries = true)
        {
            if (collection == null)
            {
                return string.Empty;
            }

            converter = converter ?? (x => x == null ? string.Empty : x.ToString());
            if (sortCollection)
            {
                collection = collection.OrderBy(converter, StringComparer.OrdinalIgnoreCase);
            }

            var sb = new StringBuilder();
            int index = 0;
            bool added = false;
            IEnumerator<T> enumerator = collection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (maxItems == null || index < maxItems)
                {
                    string itemString = converter(enumerator.Current);
                    if (!ignoreEmptyEntries || !string.IsNullOrEmpty(itemString))
                    {
                        if (added)
                        {
                            sb.Append(separator);
                        }

                        sb.Append(itemString);
                        added = true;
                        if (index == maxItems - 1)
                        {
                            break;
                        }

                        index++;
                    }
                }
            }

            if (maxItems != null)
            {
                int remainingItems = 0;
                while (enumerator.MoveNext())
                {
                    if (!ignoreEmptyEntries || !string.IsNullOrEmpty(converter(enumerator.Current)))
                    {
                        remainingItems++;
                    }
                }

                if (remainingItems > 0)
                {
                    sb.AppendFormat(additionalItemsCountFormat, remainingItems);
                }
            }

            return sb.ToString();
        }
    }
}