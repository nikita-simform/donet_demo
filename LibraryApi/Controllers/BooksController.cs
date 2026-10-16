using LibraryApi.Configuration;
using LibraryApi.Models;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LibraryApi.Controllers;

// DAY 3: CONTROLLERS + ROUTING
// [Route("api/[controller]")] -> "[controller]" becomes "books" (from "BooksController"),
// so every action below is under /api/books.
[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly ILibraryService _libraryService;
    private readonly ApiSettings _apiSettings;

    // DI: the service AND the bound configuration are both injected —
    // the controller never 'new's up either one.
    public BooksController(ILibraryService libraryService, IOptions<ApiSettings> apiSettings)
    {
        _libraryService = libraryService;
        _apiSettings = apiSettings.Value;
    }

    [HttpGet("settings")] // GET /api/books/settings
    public ActionResult<ApiSettings> GetSettings() => Ok(_apiSettings);

    [HttpGet] // GET /api/books
    public async Task<ActionResult<List<BookEntity>>> GetAll()
    {
        var books = await _libraryService.GetAllAsync();
        return Ok(books);
    }

    [HttpGet("{id}")] // GET /api/books/5
    public async Task<ActionResult<BookEntity>> GetById(int id)
    {
        var book = await _libraryService.GetByIdAsync(id);
        return book is null ? NotFound() : Ok(book);
    }

    [HttpPost] // POST /api/books
    public async Task<ActionResult<BookEntity>> Create(BookEntity book)
    {
        var created = await _libraryService.AddAsync(book);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPost("{id}/borrow")] // POST /api/books/5/borrow
    public async Task<IActionResult> Borrow(int id)
    {
        var success = await _libraryService.BorrowAsync(id);
        return success ? NoContent() : BadRequest("Book is not available to borrow.");
    }
}
