// DEPENDENCY INJECTION: Library depends on the INotifier ABSTRACTION,
// not a concrete class. Whoever constructs a Library decides which
// implementation it gets. Library itself never calls "new ConsoleNotifier()".
public class Library
{
    // ENCAPSULATION: the catalog is private; callers only get read-only access via Items.
    private readonly List<LibraryItem> catalog = new();
    private readonly INotifier notifier;

    public IReadOnlyList<LibraryItem> Items => catalog;

    // Constructor injection — the classic form of DI.
    public Library(INotifier notifier)
    {
        this.notifier = notifier;
    }

    public void Add(LibraryItem item)
    {
        catalog.Add(item);
        notifier.Notify($"Added to catalog: {item.Describe()}");
    }

    public void Borrow(string title)
    {
        var item = catalog.FirstOrDefault(i => i.Title == title); // var: compiler infers LibraryItem?
        if (item is null)
        {
            notifier.Notify($"No such item: {title}");
            return;
        }

        item.MarkBorrowed();
        notifier.Notify($"Borrowed: {item.Describe()}");
    }

    // ASYNC / TASK<T> / AWAIT: simulates calling a remote catalog service
    // (e.g. a publisher's API) without blocking the calling thread while "waiting".
    public async Task<List<LibraryItem>> FetchNewArrivalsAsync()
    {
        await Task.Delay(1000); // stands in for a real network call

        return new List<LibraryItem>
        {
            // OBJECT INITIALIZER style construction (via constructor + initializer mix)
            new Book("Clean Code", 2008, 35.00m, "Robert C. Martin"),
            new Magazine("National Geographic", 2026, 8.50m, 301),
        };
    }
}
