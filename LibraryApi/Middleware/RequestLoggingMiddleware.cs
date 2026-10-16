using System.Diagnostics;

namespace LibraryApi.Middleware;

// DAY 3: MIDDLEWARE
// Custom middleware class — the "full" pattern (vs an inline app.Use(...) lambda).
// Registered in Program.cs with app.UseMiddleware<RequestLoggingMiddleware>().
// Everything before 'await _next(context)' runs on the way IN; everything
// after it runs on the way OUT, once the rest of the pipeline has finished.
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        _logger.LogInformation("--> {Method} {Path}", context.Request.Method, context.Request.Path);

        await _next(context); // hands off to the next thing in the pipeline (routing, auth, the controller...)

        stopwatch.Stop();
        _logger.LogInformation(
            "<-- {StatusCode} ({ElapsedMs}ms)",
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds);
    }
}
