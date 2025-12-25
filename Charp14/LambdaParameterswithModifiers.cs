using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Charp14
{
    internal class LambdaParameterswithModifiers
    {
        delegate bool TryOperation<T>(ref T state);
        struct LargeStruct
        {
            public long A, B, C, D, E, F, G, H;
        }
        delegate long ComputeSum(in LargeStruct data);


        //ref Modifier – Mutating Values Inline
        void RefModifier()
        {

            int counter = 0;
            TryOperation<int> incrementAndCheck = (ref value) =>
            {
                value++;
                return value < 5;
            };

            while (incrementAndCheck(ref counter))
            {
                Console.WriteLine($"Counter: {counter}");
            }
            // Output: Counter: 1 ... Counter: 4
            Console.WriteLine(counter); // 5
        }
        //in Modifier – Read-Only Reference for Large Structs
        void InModifier()
        {
            ComputeSum sumFields = (in LargeStruct data) =>
            {
                return data.A + data.B + data.C + data.D +
                       data.E + data.F + data.G + data.H;
            };
            LargeStruct large = new LargeStruct
            {
                A = 1, B = 2, C = 3, D = 4,
                E = 5, F = 6, G = 7, H = 8
            };
            long total = sumFields(in large);
            Console.WriteLine($"Total Sum: {total}"); // Total Sum: 36
        }

        delegate bool TryDivideDelegate(int numerator, int denominator, out int result);

         //out Modifier – Returning Multiple Values via Lambda
            void OutModifier()
            {
                TryDivideDelegate tryDivide = (int numerator, int denominator, out int result) =>
                {
                    if (denominator == 0)
                    {
                        result = 0;
                        return false;
                    }
                    result = numerator / denominator;
                    return true;
                };
            if (tryDivide(10, 2, out int divisionResult))
            {
                Console.WriteLine($"Division Result: {divisionResult}"); // Division Result: 5
            }
            else
            {
                Console.WriteLine("Division by zero.");
            }
        }

            //params Modifier – Variable Number of Arguments
            delegate int[] GetLengthsDelegate(params string[] inputs);

            GetLengthsDelegate getLengths = (params string[] inputs) =>
                {
                    int[] lengths = new int[inputs.Length];
                    for (int i = 0; i < inputs.Length; i++)
                    {
                        lengths[i] = inputs[i].Length;
                    }
                    return lengths;
                };
            void ParamsModifier()
            {
            
                int[] lengthsResult = getLengths("apple", "banana", "cherry");
                Console.WriteLine(string.Join(", ", lengthsResult)); // Output: 5, 6, 6
            }

        // 5. Combining Multiple Modifiers
        delegate bool ProcessDataDelegate(in LargeStruct input, ref int counter, out long sum);

        void CombiningMultipleModifiers()
        {
            ProcessDataDelegate processData = (in LargeStruct input, ref int counter, out long sum) =>
            {
                counter++;
                sum = input.A + input.B + input.C + input.D +
                      input.E + input.F + input.G + input.H;
                return sum > 0;
            };

            LargeStruct data = new LargeStruct
            {
                A = 10, B = 20, C = 30, D = 40,
                E = 50, F = 60, G = 70, H = 80
            };

            int processCount = 0;
            if (processData(in data, ref processCount, out long totalSum))
            {
                Console.WriteLine($"Process Count: {processCount}"); // Process Count: 1
                Console.WriteLine($"Total Sum: {totalSum}"); // Total Sum: 360
            }

            if (processData(in data, ref processCount, out long totalSum2))     
            {
                Console.WriteLine($"Process Count: {processCount}"); // Process Count: 2
                Console.WriteLine($"Total Sum: {totalSum2}"); // Total Sum: 360
            }
        }

        //6. scoped Modifier – Restrict Span Lifetime
        delegate void ProcessSpanDelegate(scoped ReadOnlySpan<int> data);

        void ScopedModifier()
        {
            ProcessSpanDelegate processSpan = (scoped ReadOnlySpan<int> data) =>
            {
                int sum = 0;
                foreach (var item in data)
                {
                    sum += item;
                }
                Console.WriteLine($"Sum of span elements: {sum}");
                // The 'scoped' ensures this span doesn't escape the lambda scope
            };

            Span<int> numbers = stackalloc int[] { 1, 2, 3, 4, 5 };
            processSpan(numbers);
            // Output: Sum of span elements: 15
        }

        //7. ref readonly – Safe Read-Only Reference
        delegate string FormatDataDelegate(ref readonly LargeStruct data);

        void RefReadonlyModifier()
        {
            FormatDataDelegate formatData = (ref readonly LargeStruct data) =>
            {
                // Can read but cannot modify
                long total = data.A + data.B + data.C + data.D +
                           data.E + data.F + data.G + data.H;
                return $"Total: {total}, First: {data.A}, Last: {data.H}";
            };

            LargeStruct myData = new LargeStruct
            {
                A = 100, B = 200, C = 300, D = 400,
                E = 500, F = 600, G = 700, H = 800
            };

            string result = formatData(ref myData);
            Console.WriteLine(result);
            // Output: Total: 3600, First: 100, Last: 800
        }

        //8. Event Handler with ref for State Machines
        delegate void StateTransitionDelegate(ref int state, string action);

        void EventHandlerWithRef()
        {
            StateTransitionDelegate handleTransition = (ref int state, string action) =>
            {
                Console.WriteLine($"Current State: {state}, Action: {action}");
                
                switch (action)
                {
                    case "increment":
                        state++;
                        break;
                    case "decrement":
                        state--;
                        break;
                    case "reset":
                        state = 0;
                        break;
                }
                
                Console.WriteLine($"New State: {state}");
            };

            int machineState = 0;
            handleTransition(ref machineState, "increment");
            // Output: Current State: 0, Action: increment
            //         New State: 1
            
            handleTransition(ref machineState, "increment");
            // Output: Current State: 1, Action: increment
            //         New State: 2
            
            handleTransition(ref machineState, "decrement");
            // Output: Current State: 2, Action: decrement
            //         New State: 1
            
            handleTransition(ref machineState, "reset");
            // Output: Current State: 1, Action: reset
            //         New State: 0
        }

        //9. Async Lambda with in Parameter (Performance Win)
        // Note: Async methods cannot have ref/in/out parameters directly in C#
        // Alternative: Pass by value or use a wrapper class
        delegate Task<long> AsyncComputeDelegate(LargeStruct data, int multiplier);

        async Task AsyncLambdaWithInParameter()
        {
            AsyncComputeDelegate asyncCompute = async (LargeStruct data, int multiplier) =>
            {
                // Simulate async operation
                await Task.Delay(100);
                
                long sum = data.A + data.B + data.C + data.D +
                          data.E + data.F + data.G + data.H;
                
                return sum * multiplier;
            };

            LargeStruct largeData = new LargeStruct
            {
                A = 1, B = 2, C = 3, D = 4,
                E = 5, F = 6, G = 7, H = 8
            };

            long result = await asyncCompute(largeData, 10);
            Console.WriteLine($"Async Result: {result}");
            // Output: Async Result: 360
            
            result = await asyncCompute(largeData, 5);
            Console.WriteLine($"Async Result: {result}");
            // Output: Async Result: 180
        }

    }
}
