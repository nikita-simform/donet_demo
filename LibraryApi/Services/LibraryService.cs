using LibraryApi.Data;
using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Services;

// DAY 3: SERVICES + DATABASE LAYER
// Business logic lives here, not in the controller. LibraryDbContext is
// injected via DI (constructor injection) — this class never calls 'new' on it.
public class LibraryService : ILibraryService
{
    private readonly LibraryDbContext _context;

    public LibraryService(LibraryDbContext context)
    {
        _context = context;
    }

    // ToListAsync() (EF Core) instead of ToList() — frees the thread while the
    // database responds, instead of blocking it. Ties back to Day 2.
    public async Task<List<BookEntity>> GetAllAsync() =>
        await _context.Books.ToListAsync();

    public async Task<BookEntity?> GetByIdAsync(int id) =>
        await _context.Books.FindAsync(id);

    public async Task<BookEntity> AddAsync(BookEntity book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<bool> BorrowAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book is null || book.Status != BookStatus.Available)
            return false;

        book.Status = BookStatus.Borrowed;
        await _context.SaveChangesAsync();
        return true;
    }
}
