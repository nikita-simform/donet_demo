// DAY 2: RECORDS
// A record is a reference type built for immutable VALUE-based data.
// Two records with the same property values are considered equal —
// unlike a regular class, which compares by reference.

// Positional record — properties declared right in the parameter list, all init-only.
public record BookSummary(string Title, string Author, int Year);

public static class RecordsDemo
{
    public static void Run()
    {
        Console.WriteLine("=== DAY 2: RECORDS ===");

        var a = new BookSummary("Dune", "Frank Herbert", 1965);
        var b = new BookSummary("Dune", "Frank Herbert", 1965);

        Console.WriteLine($"a == b : {a == b}");           // True — compares VALUES, not references
        Console.WriteLine($"ReferenceEquals(a, b): {ReferenceEquals(a, b)}"); // False — still two objects

        // 'with' expression: copy a record, changing only the properties you specify.
        // The original 'a' is untouched — this is how you "modify" an immutable value.
        var reprint = a with { Year = 2026 };
        Console.WriteLine($"original: {a}");
        Console.WriteLine($"reprint : {reprint}");

        // a.Year = 1990;  // COMPILE ERROR — positional record properties are init-only (immutable)

        Console.WriteLine();
    }
}
