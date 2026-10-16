// INHERITANCE: Book "is a" LibraryItem.
public class Book : LibraryItem
{
    public string Author { get; set; }

    // NULLABLE REFERENCE TYPES: notes are optional — this can legitimately be null.
    public string? Notes { get; set; }

    // CONSTRUCTOR
    public Book(string title, int year, decimal replacementCost, string author)
        : base(title, year, replacementCost)
    {
        Author = author;
    }

    // INHERITANCE + override: specialized description for a Book.
    public override string Describe()
    {
        // base.Describe() calls the parent version, then we add to it.
        var notesPart = Notes is null ? "" : $" — note: {Notes}";
        return $"{base.Describe()} by {Author}{notesPart}";
    }
}
