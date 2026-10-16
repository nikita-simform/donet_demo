// DAY 2: LINQ + DEFERRED EXECUTION

public static class LinqDemo
{
    public static void Run()
    {
        Console.WriteLine("=== DAY 2: LINQ ===");

        var prices = new List<decimal> { 35.00m, 14.00m, 42.50m, 6.00m, 12.00m };

        // Method syntax (most common in real code)
        var affordable = prices
            .Where(p => p < 20m)
            .OrderBy(p => p)
            .ToList();
        Console.WriteLine($"Affordable (method syntax): {string.Join(", ", affordable)}");

        // Query syntax — same result, SQL-like shape
        var affordable2 = from p in prices
                           where p < 20m
                           orderby p
                           select p;
        Console.WriteLine($"Affordable (query syntax): {string.Join(", ", affordable2)}");

        Console.WriteLine($"Total: {prices.Sum():C}");
        Console.WriteLine($"Any over 40? {prices.Any(p => p > 40m)}");
        Console.WriteLine($"Cheapest: {prices.Min():C}");

        Console.WriteLine();
    }
}

public static class DeferredExecutionDemo
{
    public static void Run()
    {
        Console.WriteLine("=== DAY 2: DEFERRED EXECUTION ===");

        var numbers = new List<int> { 1, 2, 3 };

        // Building this query does NOT run the Where predicate yet.
        var query = numbers.Where(n =>
        {
            Console.WriteLine($"  (checking {n})");
            return n > 1;
        });

        Console.WriteLine("Query built. Nothing printed above yet because nothing enumerated it.");

        // Mutate the source BEFORE enumerating — deferred execution means the query
        // sees this change, because it only reads 'numbers' when actually run.
        numbers.Add(4);
        Console.WriteLine("Added 4 to the source list before running the query.");

        Console.WriteLine("Now calling ToList() — this is what actually executes it:");
        var result = query.ToList(); // <-- "(checking 1)" etc. print only now, and 4 IS included
        Console.WriteLine($"Result: {string.Join(", ", result)}");

        Console.WriteLine();
    }
}
