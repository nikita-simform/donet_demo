// DAY 2: GENERICS
// Write one class/method that works with any type, decided at the call site,
// without losing type safety (no casting, no boxing).

// 'where T : LibraryItem' is a CONSTRAINT — T must be a LibraryItem or subclass.
// This lets Repository<T> call LibraryItem members (like .Title) on T directly.
public class Repository<T> where T : LibraryItem
{
    private readonly List<T> items = new();

    public void Add(T item) => items.Add(item);

    public IReadOnlyList<T> GetAll() => items;

    public T? FindByTitle(string title) =>
        items.FirstOrDefault(i => i.Title == title);
}

public static class GenericsDemo
{
    // Generic method with an IComparable<T> constraint — works for ints, strings,
    // dates, or any type that knows how to compare itself.
    public static T Max<T>(T a, T b) where T : IComparable<T> =>
        a.CompareTo(b) >= 0 ? a : b;

    public static void Run()
    {
        Console.WriteLine("=== DAY 2: GENERICS ===");

        // Repository<Book> only accepts Books; Repository<Magazine> only Magazines.
        var bookRepo = new Repository<Book>();
        bookRepo.Add(new Book("1984", 1949, 15.00m, "George Orwell"));
        bookRepo.Add(new Book("Brave New World", 1932, 14.00m, "Aldous Huxley"));

        Console.WriteLine($"Books in repo: {bookRepo.GetAll().Count}");
        var found = bookRepo.FindByTitle("1984");
        Console.WriteLine($"Found by title: {found?.Describe()}");

        Console.WriteLine($"Max(3, 7) = {Max(3, 7)}");
        Console.WriteLine($"Max(\"apple\", \"banana\") = {Max("apple", "banana")}");

        Console.WriteLine();
    }
}
