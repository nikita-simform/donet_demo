using LibraryApi.Models;

namespace LibraryApi.Services;

// DAY 3: SERVICES
// The interface controllers depend on — lets the controller stay ignorant
// of HOW books are fetched/stored (EF Core, a different DB, a fake for tests...).
public interface ILibraryService
{
    Task<List<BookEntity>> GetAllAsync();
    Task<BookEntity?> GetByIdAsync(int id);
    Task<BookEntity> AddAsync(BookEntity book);
    Task<bool> BorrowAsync(int id);
}
