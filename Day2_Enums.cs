// DAY 2: ENUMS

// Plain enum — a named set of integer constants.
public enum LibraryItemStatus
{
    Available,  // 0
    Borrowed,   // 1
    Reserved,   // 2
    Lost        // 3
}

// [Flags] enum — values are powers of two so they can be combined with bitwise OR.
[Flags]
public enum AccessPermissions
{
    None = 0,
    Read = 1,
    Write = 2,
    Delete = 4,
    Admin = Read | Write | Delete   // a named combination
}

public static class EnumsDemo
{
    public static void Run()
    {
        Console.WriteLine("=== DAY 2: ENUMS ===");

        var status = LibraryItemStatus.Borrowed;
        Console.WriteLine($"status = {status} (underlying int: {(int)status})");

        var parsed = Enum.Parse<LibraryItemStatus>("Reserved");
        Console.WriteLine($"Parsed 'Reserved' -> {parsed}");

        bool isValid = Enum.TryParse<LibraryItemStatus>("NotAStatus", out var result);
        Console.WriteLine($"TryParse('NotAStatus') succeeded: {isValid}");

        // Flags in action
        var perms = AccessPermissions.Read | AccessPermissions.Write;
        Console.WriteLine($"perms = {perms}");                         // "Read, Write"
        Console.WriteLine($"perms.HasFlag(Write): {perms.HasFlag(AccessPermissions.Write)}");
        Console.WriteLine($"perms.HasFlag(Delete): {perms.HasFlag(AccessPermissions.Delete)}");

        Console.WriteLine();
    }
}
