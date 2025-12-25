using System;
using System.Collections.Generic;
using System.Text;

namespace Charp14
{
    //1. Direct Parameter Passing (No .AsSpan() Needed)
    //example please

    public class SpanSupport
    {
        static void Process(ReadOnlySpan<int> data)
        {
            Console.WriteLine($"Length: {data.Length}, First: {data[0]}");
        }

        static void Modify(Span<byte> buffer)
        {
            buffer[0] = 255;
        }

        public void Main()
        {
            // Before C# 14: Required explicit conversion
            int[] numbers = { 10, 20, 30 };
            Process(numbers.AsSpan());  // Verbose

            // In C# 14: Implicit!
            Process(numbers);  // array → ReadOnlySpan<int>

            byte[] bytes = new byte[10];
            Modify(bytes);     // array → Span<byte>

            string message = "Hello World!";
            int vowels = CountVowels(message);  // string → ReadOnlySpan<char> implicitly
            Console.WriteLine(vowels);

            string path = "/users/documents/file.txt";
            bool isAbsolute = path.StartsWith("/");  // Calls extension directly on string!
            Console.WriteLine(isAbsolute);

            // No need for type arguments
            int[] values = { 42, 84 };
            int first = values.FirstOrDefault(0);  // Infers T = int from array
            Console.WriteLine(first);             // 42

            int[] empty = Array.Empty<int>();
            Console.WriteLine(empty.FirstOrDefault(-1));  // -1

            Fill(stackalloc int[5]);  // stackalloc → Span<int> implicitly

            int[] arr = { 1, 2, 3 };
            Fill(arr[1..]);

            string input = "1A3F";
            int result = ParseHex(input);  // Direct string pass
            Console.WriteLine(result);     // 6719

            string[] words = { "apple", "banana" };
            PrintObjects(words);  // string[] → ReadOnlySpan<object> via reference conversion
        }

        static int CountVowels(ReadOnlySpan<char> text)
        {
            int count = 0;
            foreach (char c in text)
                if ("aeiouAEIOU".Contains(c)) count++;
            return count;
        }

        static T FirstOrDefault<T>(this ReadOnlySpan<T> source, T fallback)
        => source.Length > 0 ? source[0] : fallback;

        static void Fill(Span<int> span)
        {
            for (int i = 0; i < span.Length; i++)
                span[i] = i * 10;
        }
        static int ParseHex(ReadOnlySpan<char> hex)
        {
            int value = 0;
            foreach (char c in hex)
            {
                value *= 16;
                value += c switch
                {
                    >= '0' and <= '9' => c - '0',
                    >= 'A' and <= 'F' => c - 'A' + 10,
                    >= 'a' and <= 'f' => c - 'a' + 10,
                    _ => throw new FormatException()
                };
            }
            return value;
        }

        static void PrintObjects(ReadOnlySpan<object> items)
        {
            foreach (var item in items) Console.WriteLine(item);
        }

    }
    static class Extensions
    {
        public static bool StartsWith(this ReadOnlySpan<char> span, ReadOnlySpan<char> value) =>
            span.StartsWith(value);
    }



   
}
