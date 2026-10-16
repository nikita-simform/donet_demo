// INTERFACE: a contract. Any class that implements this MUST provide Notify().
// Used below for DEPENDENCY INJECTION — Library doesn't care HOW notification
// happens, only that something implements INotifier.
public interface INotifier
{
    void Notify(string message);
}

// One concrete implementation: prints to the console.
public class ConsoleNotifier : INotifier
{
    public void Notify(string message) => Console.WriteLine($"[notify] {message}");
}

// A second implementation, to prove the interface lets us swap behavior freely.
public class SilentNotifier : INotifier
{
    public List<string> Log { get; } = new();
    public void Notify(string message) => Log.Add(message);
}
