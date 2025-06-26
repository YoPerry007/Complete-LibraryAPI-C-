using LibraryAPI.Models;
using libraryAPI.Services.Interface;
using LibraryAPI.Contracts.Request;
using Microsoft.AspNetCore.Mvc;
using libraryAPI.Models;
using Mapster;
using LibraryAPI.Contracts.Response;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;

    public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = await _authorRepository.GetById(request.AuthorId);
            if (author == null)
            {
                return NotFound();
            }

            var book = new Book
            {
                Title = request.Title,
                AuthorId = request.AuthorId
            };
            var createdBook = await _bookRepository.Create(book);
            return Ok(createdBook.Adapt<BookResponse>());
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }

    }
    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        try
        {
            var books = await _bookRepository.GetAll();
            if (books == null)
            {
                return NotFound("No books found.");
            }
            return Ok(books.Adapt<IEnumerable<BookResponse>>());
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]

    public async Task<IActionResult> GetBooksById(Guid id)
    {
        try
        {
            var book = await _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book.Adapt<IEnumerable<BookResponse>>());
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("author/{authorId}")]
    public async Task<IActionResult> GetBooksByAuthorId(Guid authorId)
    {
        try
        {
            var book = await _bookRepository.GetByAuthorId(authorId);
            if (book == null || !book.Any())
            {
                return NotFound($"No books found for author with id {authorId}");
            }
            return Ok(book.Adapt<IEnumerable<BookResponse>>());
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBookRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (string.IsNullOrEmpty(request.AuthorId))
            {
                return BadRequest("Author is required.");
            }
            Guid authorId = Guid.Parse(request.AuthorId);
            var author = await _authorRepository.GetById(authorId);
            if (author == null)
            {
                return NotFound();
            }

            var book = new Book
            {
                Title = request.Title,
                AuthorId = authorId
            };
            var updatedBook = await _bookRepository.UpdateById(id, book);
            if (updatedBook == null)
            {
                return NotFound();
            }
            return Ok(updatedBook.Adapt<BookResponse>());
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        try
        {
            var deleted = await _bookRepository.DeleteById(id);
            if ((bool)!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}