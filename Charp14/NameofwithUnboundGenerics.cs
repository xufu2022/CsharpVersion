using System;
using System.Collections.Generic;
using System.Text;

namespace Charp14
{
    internal class NameofwithUnboundGenerics
    {
        // 1. Logging or Diagnostics in Generic Classes/Methods
        class Repository<T>
        {
            public void LogOperation()
            {
                // C# 14: Can use nameof with unbound generic types
                Console.WriteLine($"Repository type: {nameof(Repository<>)}");
                Console.WriteLine($"Full name: {nameof(Repository<T>)}");
                Console.WriteLine($"Item type: {typeof(T).Name}");
            }

            public void Add(T item)
            {
                Console.WriteLine($"Adding item to {nameof(Repository<>)}");
            }
        }

        void LoggingExample()
        {
            var stringRepo = new Repository<string>();
            stringRepo.LogOperation();
            // Output: Repository type: Repository`1
            //         Full name: Repository`1
            //         Item type: String

            var intRepo = new Repository<int>();
            intRepo.Add(42);
            // Output: Adding item to Repository`1
        }

        // 2. Exception Messages with Generic Type Names
        class DataProcessor<TInput, TOutput>
        {
            public TOutput Process(TInput input)
            {
                if (input == null)
                {
                    throw new ArgumentNullException(
                        nameof(input),
                        $"Cannot process null input in {nameof(DataProcessor<,>)}"
                    );
                }

                // Processing logic here
                Console.WriteLine($"Processing in {nameof(DataProcessor<TInput, TOutput>)}");
                return default(TOutput);
            }

            public void ValidateConfiguration()
            {
                Console.WriteLine($"Validating configuration for {nameof(DataProcessor<,>)}");
                Console.WriteLine($"Input type: {typeof(TInput).Name}");
                Console.WriteLine($"Output type: {typeof(TOutput).Name}");
            }
        }

        void ExceptionExample()
        {
            var processor = new DataProcessor<string, int>();
            processor.ValidateConfiguration();
            // Output: Validating configuration for DataProcessor`2
            //         Input type: String
            //         Output type: Int32

            try
            {
                processor.Process(null);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                // Output: Exception: Cannot process null input in DataProcessor`2
            }
        }

        // 3. Reflection or Attribute Arguments
        class Cache<TKey, TValue>
        {
            private Dictionary<TKey, TValue> _data = new Dictionary<TKey, TValue>();

            public void LogCacheInfo()
            {
                var typeName = nameof(Cache<,>);
                Console.WriteLine($"Cache Type Name: {typeName}");
                Console.WriteLine($"Generic Type Definition: {typeof(Cache<,>).Name}");
                Console.WriteLine($"Current Instance: {nameof(Cache<TKey, TValue>)}");
            }

            public string GetCacheIdentifier()
            {
                return $"{nameof(Cache<,>)}<{typeof(TKey).Name},{typeof(TValue).Name}>";
            }
        }

        void ReflectionExample()
        {
            var cache = new Cache<int, string>();
            cache.LogCacheInfo();
            // Output: Cache Type Name: Cache`2
            //         Generic Type Definition: Cache`2
            //         Current Instance: Cache`2

            string identifier = cache.GetCacheIdentifier();
            Console.WriteLine($"Cache Identifier: {identifier}");
            // Output: Cache Identifier: Cache`2<Int32,String>
        }

        // 4. Accessing Members of Generic Types
        class Container<T>
        {
            public T Value { get; set; }

            public void LogMemberInfo()
            {
                // Get names of type and members
                Console.WriteLine($"Container type: {nameof(Container<>)}");
                Console.WriteLine($"Value property: {nameof(Container<T>.Value)}");
                Console.WriteLine($"LogMemberInfo method: {nameof(Container<T>.LogMemberInfo)}");
            }

            public static void StaticMethod()
            {
                Console.WriteLine($"Static method in {nameof(Container<>)}");
            }
        }

        class GenericService<T1, T2, T3>
        {
            public void DisplayInfo()
            {
                Console.WriteLine($"Service: {nameof(GenericService<,,>)}");
                Console.WriteLine($"Type parameters: {typeof(T1).Name}, {typeof(T2).Name}, {typeof(T3).Name}");
            }
        }

        void MemberAccessExample()
        {
            var container = new Container<string>();
            container.LogMemberInfo();
            // Output: Container type: Container`1
            //         Value property: Value
            //         LogMemberInfo method: LogMemberInfo

            Container<int>.StaticMethod();
            // Output: Static method in Container`1

            var service = new GenericService<int, string, bool>();
            service.DisplayInfo();
            // Output: Service: GenericService`3
            //         Type parameters: Int32, String, Boolean
        }

        // 5. In Source Generators or Analyzers (Simulation)
        static class DiagnosticHelper
        {
            public static string GetGenericTypeDiagnostic<T>()
            {
                return $"Analyzing generic type: {nameof(List<>)}";
            }

            public static string GetDictionaryDiagnostic()
            {
                return $"Dictionary base type: {nameof(Dictionary<,>)}";
            }

            public static void ReportGenericUsage<TKey, TValue>()
            {
                Console.WriteLine($"Generic type usage detected:");
                Console.WriteLine($"  Type: {nameof(Dictionary<,>)}");
                Console.WriteLine($"  Key: {typeof(TKey).Name}");
                Console.WriteLine($"  Value: {typeof(TValue).Name}");
            }
        }

        void SourceGeneratorExample()
        {
            string diagnostic1 = DiagnosticHelper.GetGenericTypeDiagnostic<string>();
            Console.WriteLine(diagnostic1);
            // Output: Analyzing generic type: List`1

            string diagnostic2 = DiagnosticHelper.GetDictionaryDiagnostic();
            Console.WriteLine(diagnostic2);
            // Output: Dictionary base type: Dictionary`2

            DiagnosticHelper.ReportGenericUsage<int, string>();
            // Output: Generic type usage detected:
            //           Type: Dictionary`2
            //           Key: Int32
            //           Value: String
        }

        // 6. Advanced: Nested Generic Types
        class Outer<T>
        {
            public class Inner<U>
            {
                public void LogNestedInfo()
                {
                    Console.WriteLine($"Outer type: {nameof(Outer<>)}");
                    Console.WriteLine($"Inner type: {nameof(Inner<>)}");
                    Console.WriteLine($"Full nested: {nameof(Outer<T>.Inner<U>)}");
                }
            }
        }

        void NestedGenericExample()
        {
            var nested = new Outer<string>.Inner<int>();
            nested.LogNestedInfo();
            // Output: Outer type: Outer`1
            //         Inner type: Inner`1
            //         Full nested: Inner`1
        }

        // Main method to run all examples
        public void RunAllExamples()
        {
            Console.WriteLine("=== 1. Logging or Diagnostics Example ===");
            LoggingExample();
            Console.WriteLine();

            Console.WriteLine("=== 2. Exception Messages Example ===");
            ExceptionExample();
            Console.WriteLine();

            Console.WriteLine("=== 3. Reflection Example ===");
            ReflectionExample();
            Console.WriteLine();

            Console.WriteLine("=== 4. Member Access Example ===");
            MemberAccessExample();
            Console.WriteLine();

            Console.WriteLine("=== 5. Source Generator Example ===");
            SourceGeneratorExample();
            Console.WriteLine();

            Console.WriteLine("=== 6. Nested Generic Example ===");
            NestedGenericExample();
        }
    }
}
