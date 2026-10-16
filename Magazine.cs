// INHERITANCE: another LibraryItem subclass, proving one base can have many shapes.
public class Magazine : LibraryItem
{
    public int IssueNumber { get; set; }

    public Magazine(string title, int year, decimal replacementCost, int issueNumber)
        : base(title, year, replacementCost)
    {
        IssueNumber = issueNumber;
    }

    public override string Describe() => $"{base.Describe()}, issue #{IssueNumber}";
}
