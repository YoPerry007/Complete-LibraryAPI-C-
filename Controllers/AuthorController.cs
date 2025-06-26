using libraryAPI.Services.Interface;
using LibraryAPI.Contracts.Request;
using LibraryAPI.Models;
using LibraryAPI.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorController(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }


    [HttpPost]
    public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = new Author
            {
                Name = request.Name
            };
            var CreateAuthor = await _authorRepository.Create(author);
            return Ok(CreateAuthor);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        try
        {
            var authors = await _authorRepository.GetAll();
            return Ok(authors);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorById(Guid id)
    {
        try
        {
            var author = await _authorRepository.GetById(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody] UpdateAuthorRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = new Author
            {
                Name = request.Name
            };
            var updatedAuthor = await _authorRepository.UpdateById(id, author);
            if (updatedAuthor == null)
            {
                return NotFound();
            }
            return Ok(updatedAuthor);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");

        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
        try
        {
            var isDeleted = await _authorRepository.DeleteById(id);
            if ((bool)!isDeleted)
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