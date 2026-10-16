namespace LibraryApi.Models;

// DAY 3: this is the shape of a row in the "database" (EF Core InMemory provider).
public enum BookStatus
{
    Available,
    Borrowed,
    Lost
}

public class BookEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public int Year { get; set; }
    public BookStatus Status { get; set; } = BookStatus.Available;
}
