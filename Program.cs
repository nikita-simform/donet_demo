// Top-level statements file — this IS the Main method. It can be async
// directly, which is why we can use `await` below without wrapping it.

Console.WriteLine("=== BASIC TYPES ===");
string libraryName = "Ubuntu City Library"; // string
int foundedYear = 1995;                     // int
bool isOpenNow = true;                      // bool
decimal lateFeePerDay = 0.25m;               // decimal — money, use 'm' suffix
double averageVisitTimeHours = 1.5;          // double — general math
char section = 'B';                          // char
Console.WriteLine($"{libraryName}, founded {foundedYear}, open={isOpenNow}, " +
                   $"late fee={lateFeePerDay}/day, avg visit={averageVisitTimeHours}h, section={section}");

Console.WriteLine("\n=== var vs static typing ===");
var count = 3;        // compiler infers 'int' here — fixed forever, same as static typing
Console.WriteLine($"count is inferred as: {count.GetType().Name}");

Console.WriteLine("\n=== DEPENDENCY INJECTION in action ===");
// We choose which INotifier implementation Library gets — Library has no idea which one it is.
INotifier notifier = new ConsoleNotifier();
var library = new Library(notifier); // constructor injection

Console.WriteLine("\n=== Classes, constructors, inheritance, encapsulation ===");
var book = new Book("The Pragmatic Programmer", 1999, 42.50m, "Hunt & Thomas")
{
    Notes = "Classic — recommended for all devs" // object initializer setting a property after construction
};
var magazine = new Magazine("Wired", 2026, 6.00m, 150);

library.Add(book);
library.Add(magazine);

Console.WriteLine("\n=== Nullable reference types ===");
string? maybeNull = null;
Console.WriteLine($"maybeNull?.Length (safe even though null): {maybeNull?.Length}");
// book.Notes is string? — we handled the null case inside Book.Describe() already.

Console.WriteLine("\n=== Interfaces: swap INotifier without touching Library ===");
var silent = new SilentNotifier();
var quietLibrary = new Library(silent); // same class, different injected dependency
quietLibrary.Add(new Book("Silent Spring", 1962, 12.00m, "Rachel Carson"));
Console.WriteLine($"SilentNotifier captured {silent.Log.Count} message(s) instead of printing them.");

Console.WriteLine("\n=== Borrowing (encapsulated state transitions) ===");
library.Borrow("The Pragmatic Programmer");
try
{
    library.Borrow("The Pragmatic Programmer"); // should fail — already borrowed
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Expected error: {ex.Message}");
}

Console.WriteLine("\n=== async / Task<T> / await ===");
Console.WriteLine("Fetching new arrivals (simulated 1s network delay)...");
List<LibraryItem> newArrivals = await library.FetchNewArrivalsAsync();
foreach (var item in newArrivals)
{
    Console.WriteLine($" - {item.Describe()}");
}

Console.WriteLine("\n--- Day 1 complete ---\n");

// ============================================================
// DAY 2 — see Day2_*.cs files for the full code + comments
// ============================================================
RecordsDemo.Run();
EnumsDemo.Run();
GenericsDemo.Run();
LinqDemo.Run();
DeferredExecutionDemo.Run();
ExceptionHandlingDemo.Run();
NamingConventionsDemo.Run();
HostingDemo.Run();

Console.WriteLine("Done.");
