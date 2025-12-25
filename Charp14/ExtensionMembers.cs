using System;
using System.Collections.Generic;
using System.Text;

namespace Charp14
{
    public static class EnumerableExtensions
    {
        // Instance extension members
        extension<T>(IEnumerable<T> enumerable)
        {
            // Extension property
            public bool IsEmpty => !enumerable.Any();

            // Extension method
            public T FirstOrFallback(T fallback) => enumerable.FirstOrDefault() ?? fallback;
        }

        // Static extension members
        extension<T>(IEnumerable<T>)
        {
            // Static extension method
            public static IEnumerable<T> Range(int start, int count, Func<int, T> generator)
                => Enumerable.Range(start, count).Select(generator);

            // Static extension operator
            public static IEnumerable<T> operator +(IEnumerable<T> first, IEnumerable<T> second)
            {
                foreach (var item in first) yield return item;
                foreach (var item in second) yield return item;
            }
        }
    }

    public static class StringExtensions
    {
        extension(string str)
        {
            public bool IsEmptyOrWhiteSpace() => string.IsNullOrWhiteSpace(str);

            public string Truncate(int maxLength) =>
                str.Length <= maxLength ? str : str[..maxLength] + "...";

            public string ToSlug() =>
                str.ToLower()
                   .Replace(" ", "-")
                   .Replace("--", "-");

            public IEnumerable<string> SplitIntoWords() =>
                str.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
