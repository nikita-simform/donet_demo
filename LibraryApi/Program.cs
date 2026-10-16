using LibraryApi.Configuration;
using LibraryApi.Data;
using LibraryApi.Middleware;
using LibraryApi.Models;
using LibraryApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// SERVICE REGISTRATION — "what the app HAS" (DI container setup)
// ============================================================

// DAY 3: CONFIGURATION — bind the "ApiSettings" section of appsettings.json
// to a strongly-typed class instead of reading raw strings everywhere.
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DAY 3: DATABASE LAYER — EF Core with the InMemory provider (no real DB server
// needed to run this). Registered as the default Scoped lifetime (one per request).
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseInMemoryDatabase("LibraryDb"));

// DAY 3: SERVICES — business logic layer, registered Scoped (matches DbContext's
// lifetime — a Scoped service may safely depend on another Scoped service).
builder.Services.AddScoped<ILibraryService, LibraryService>();

var app = builder.Build();

// ============================================================
// MIDDLEWARE / REQUEST PIPELINE — "what the app DOES per request", in order
// ============================================================

// DAY 3: ENVIRONMENT-SPECIFIC CONFIGURATION — only expose Swagger in Development.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// DAY 3: MIDDLEWARE — custom logging middleware, registered first so it wraps
// everything else and can time/log the full request.
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();

// DAY 3: ROUTING — maps incoming requests to controller actions.
app.MapControllers();

// Seed some sample data so GET /api/books returns something immediately.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
    db.Books.AddRange(
        new BookEntity { Title = "Clean Code", Author = "Robert C. Martin", Year = 2008 },
        new BookEntity { Title = "The Pragmatic Programmer", Author = "Hunt & Thomas", Year = 1999 },
        new BookEntity { Title = "Dune", Author = "Frank Herbert", Year = 1965 }
    );
    db.SaveChanges();
}

app.Run();
