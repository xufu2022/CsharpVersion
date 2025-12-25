// See https://aka.ms/new-console-template for more information
using Charp14;
//extenation method ussage
// Usage
var numbers = new List<int> { 1, 2, 3 };
Console.WriteLine(numbers.IsEmpty);  // False
var combined = numbers + new[] { 4, 5 };  // Uses static operator


// Usage – feels like built-in string members
string title = "  Hello World Example  ";
Console.WriteLine(title.IsEmptyOrWhiteSpace());     // False
Console.WriteLine(title.Truncate(10));             // "  Hello Wo..."
Console.WriteLine(title.ToSlug());                 // "--hello-world-example--"
foreach (var word in title.SplitIntoWords())
    Console.WriteLine(word);                       // Hello, World, Example

Console.WriteLine("Hello, World!");
