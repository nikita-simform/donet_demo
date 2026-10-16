using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Data;

// DAY 3: DATABASE LAYER
// DbContext is the bridge between your C# classes and the database.
// Registered as SCOPED in Program.cs — one instance per HTTP request,
// so every query/save within one request shares the same unit of work.
public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

    public DbSet<BookEntity> Books => Set<BookEntity>();
}
