// INHERITANCE: common base for anything the library can hold.
// BASIC TYPES: string, int, decimal in use below.
public abstract class LibraryItem
{
    // ENCAPSULATION: private field, only exposed via a controlled property.
    private bool isBorrowed;

    public string Title { get; set; }
    public int Year { get; set; }
    public decimal ReplacementCost { get; set; } // decimal = money

    public bool IsBorrowed => isBorrowed; // read-only view of private state

    protected LibraryItem(string title, int year, decimal replacementCost)
    {
        Title = title;
        Year = year;
        ReplacementCost = replacementCost;
    }

    // ENCAPSULATION: the only way to change isBorrowed is through these methods,
    // which can enforce rules (you can't borrow something already borrowed).
    public void MarkBorrowed()
    {
        if (isBorrowed)
            throw new InvalidOperationException($"'{Title}' is already borrowed.");
        isBorrowed = true;
    }

    public void MarkReturned() => isBorrowed = false;

    // virtual = subclasses MAY override this (used for inheritance below).
    public virtual string Describe() => $"{Title} ({Year})";
}
