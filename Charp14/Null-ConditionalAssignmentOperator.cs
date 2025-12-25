using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Charp14
{
    public class Person
    {
        public string? Name { get; set; }
        public Address? Address { get; set; }
    }

    public class Address
    {
        public string? Street { get; set; }
        public string? City { get; set; }
    }

    public class NullConditionalAssignmentOperator
    {
        public static void Demo()
        {
            Person? person = null;

            // Without C# 14 – verbose null check
            if (person != null)
            {
                person.Name = "John";
            }

            // With C# 14 – clean and safe
            person?.Name = "John";                    // No assignment, no exception
            person?.Address?.Street = "123 Main St";  // Chained: safe deep assignment
        }

        public static void Main()
        {
            //compound assignment with null-conditional operator
            Person? person = null;
            person?.Name += " Doe"; // No assignment, no exception
            Console.WriteLine(person?.Name); // Output: (null)


            //customer?.Tags?.Add("Premium");  // Double null-conditional: safe even if Tags is null


            // Now imagine assigning to an event field safely:
            //EventHandler? handler = null;
            //handler?.Invoke(this, EventArgs.Empty);  // Skipped safely

            //// Or compound assignment to a nullable delegate
            //Action<string>? logger = Console.WriteLine;
            //logger?.Invoke("Hello");  // Works

            //logger = null;
            //logger?.Invoke("This won't throw");  // Skipped


            //AppSettings? settings = LoadSettings();  // Could be null if file missing

            //// Safely apply overrides
            //settings?.Database?.ConnectionString = "Server=prod;...";
            //settings?.Database?.Timeout += 15;       // Compound assignment
            //settings?.Logging?.LogLevel = "Debug";   // Nested safe update


            // JSON: { "user": null } or missing "user"
            //var response = JsonSerializer.Deserialize<ApiResponse>(json);

            //response?.User?.Profile?.Bio = "Updated bio via API";
            //response?.User?.Preferences?.Theme = "Dark";  // All safe, no NRE



        }
    }
}
