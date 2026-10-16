// DAY 2: EXCEPTION HANDLING

// Custom exception for a domain-specific error — carries more meaning than a generic Exception.
public class InsufficientStockException : Exception
{
    public string ItemTitle { get; }

    public InsufficientStockException(string itemTitle)
        : base($"No copies of '{itemTitle}' are available to borrow.")
    {
        ItemTitle = itemTitle;
    }
}

public static class ExceptionHandlingDemo
{
    private static void BorrowCopy(string title, int copiesAvailable)
    {
        if (copiesAvailable <= 0)
            throw new InsufficientStockException(title);

        Console.WriteLine($"Borrowed a copy of '{title}'. Copies left: {copiesAvailable - 1}");
    }

    public static void Run()
    {
        Console.WriteLine("=== DAY 2: EXCEPTION HANDLING ===");

        try
        {
            BorrowCopy("Clean Code", 2);
            BorrowCopy("Clean Code", 0); // this one throws
        }
        catch (InsufficientStockException ex) when (ex.ItemTitle == "Clean Code")
        {
            // exception filter ('when') — only catches THIS specific condition
            Console.WriteLine($"Filtered catch: {ex.Message}");
        }
        catch (Exception ex)
        {
            // catch-all goes LAST, after more specific catches
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
        finally
        {
            // always runs, whether an exception was thrown or not — cleanup goes here
            Console.WriteLine("Borrow attempt finished (finally block).");
        }

        // Demonstrate the wrong vs right way to re-throw
        try
        {
            try
            {
                throw new InvalidOperationException("Original failure");
            }
            catch (InvalidOperationException)
            {
                throw; // correct: 'throw;' preserves the original stack trace
                       // 'throw ex;' would reset it and hide where it really happened
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Re-thrown and caught again: {ex.Message}");
        }

        Console.WriteLine();
    }
}
