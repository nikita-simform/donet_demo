// DAY 2: APPLICATION STARTUP + DI SERVICE LIFETIMES
// Uses the Generic Host (Microsoft.Extensions.Hosting) — the same builder/DI
// pattern ASP.NET Core itself is built on, just without the web server part.
// This is also a live example of using a NuGet package (see the .csproj).

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Each implementation stamps an InstanceId at construction time so we can SEE,
// by printing Guids, whether two resolutions returned the same object or not.

public interface ISingletonService { Guid InstanceId { get; } }
public class SingletonService : ISingletonService
{
    public Guid InstanceId { get; } = Guid.NewGuid();
}

public interface IScopedService { Guid InstanceId { get; } }
public class ScopedService : IScopedService
{
    public Guid InstanceId { get; } = Guid.NewGuid();
}

public interface ITransientService { Guid InstanceId { get; } }
public class TransientService : ITransientService
{
    public Guid InstanceId { get; } = Guid.NewGuid();
}

public static class HostingDemo
{
    public static void Run()
    {
        Console.WriteLine("=== DAY 2: APPLICATION STARTUP + DI SERVICE LIFETIMES ===");

        // --- "Application startup" — building the host, registering services ---
        var builder = Host.CreateApplicationBuilder();

        builder.Services.AddSingleton<ISingletonService, SingletonService>();
        builder.Services.AddScoped<IScopedService, ScopedService>();
        builder.Services.AddTransient<ITransientService, TransientService>();

        using var host = builder.Build(); // app is "started" here — DI container is ready

        // --- "DI service lifetimes" — prove the three behaviors ---

        // SINGLETON: same instance no matter how many times or where you resolve it.
        var singletonA = host.Services.GetRequiredService<ISingletonService>();
        var singletonB = host.Services.GetRequiredService<ISingletonService>();
        Console.WriteLine($"Singleton A: {singletonA.InstanceId}");
        Console.WriteLine($"Singleton B: {singletonB.InstanceId}");
        Console.WriteLine($"Same instance? {singletonA.InstanceId == singletonB.InstanceId}  <-- always True");
        Console.WriteLine();

        // SCOPED: same instance WITHIN one scope, different instance in a new scope.
        // (In ASP.NET Core, one scope = one HTTP request.)
        using (var scope1 = host.Services.CreateScope())
        {
            var scopedA = scope1.ServiceProvider.GetRequiredService<IScopedService>();
            var scopedB = scope1.ServiceProvider.GetRequiredService<IScopedService>();
            Console.WriteLine($"Scope 1 - resolve A: {scopedA.InstanceId}");
            Console.WriteLine($"Scope 1 - resolve B: {scopedB.InstanceId}");
            Console.WriteLine($"Same within scope 1? {scopedA.InstanceId == scopedB.InstanceId}  <-- True");
        }

        using (var scope2 = host.Services.CreateScope())
        {
            var scopedC = scope2.ServiceProvider.GetRequiredService<IScopedService>();
            Console.WriteLine($"Scope 2 - resolve C: {scopedC.InstanceId}  <-- different from scope 1's instances");
        }
        Console.WriteLine();

        // TRANSIENT: a brand new instance every single time, even in the same scope.
        using (var scope3 = host.Services.CreateScope())
        {
            var transientA = scope3.ServiceProvider.GetRequiredService<ITransientService>();
            var transientB = scope3.ServiceProvider.GetRequiredService<ITransientService>();
            Console.WriteLine($"Transient A: {transientA.InstanceId}");
            Console.WriteLine($"Transient B: {transientB.InstanceId}");
            Console.WriteLine($"Same? {transientA.InstanceId == transientB.InstanceId}  <-- always False");
        }

        Console.WriteLine();
    }
}
