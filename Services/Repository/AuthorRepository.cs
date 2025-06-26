using libraryAPI.Services.Interface;
using LibraryAPI.AppDataContext;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services.Repository;


public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryDBContext _context;

    public AuthorRepository(LibraryDBContext context)
    {
        _context = context;
    }

    public async Task<Author?> Create(Author author)
    {
        var newAuthor = new Author
        {
            Name = author.Name,
            CreatedAt = DateTime.UtcNow.ToString()
        };
        _context.Authors.Add(newAuthor);
        await _context.SaveChangesAsync();
        return newAuthor;
    }

    public async Task<bool?> DeleteById(Guid id)
    {
        var targetAuthor = await _context.Authors.FindAsync(id);
        if (targetAuthor == null)
        {

            return false;
        }
        targetAuthor.DeletedAt = DateTime.UtcNow.ToString();
        _context.Authors.Update(targetAuthor);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Author>> GetAll()
    {
        return await _context.Authors
        .Where(a => a.DeletedAt == null).ToListAsync();
    }

    public async Task<Author?> GetById(Guid id)
    {
        var targetAuthor = await _context.Authors
        .Where(a => a.DeletedAt == null)
        .FirstOrDefaultAsync(a => a.Id == id);

        if (targetAuthor == null)
        {
            return null;
        }
        return targetAuthor;
    }

    public Task GetById(Guid? authorId)
    {
        throw new NotImplementedException();
    }

    public Task GetById(string? authorId)
    {
        throw new NotImplementedException();
    }

    public async Task<Author?> UpdateById(Guid id, Author author)
    {
        var targetAuthor = await _context.Authors
        .Where(a => a.DeletedAt == null)
        .FirstOrDefaultAsync(a => a.Id == id);

        if (targetAuthor == null)
        {
            return null;
        }
        targetAuthor.Name = author.Name;
        targetAuthor.UpdatedAt = DateTime.UtcNow.ToString();

        _context.Authors.Update(targetAuthor);
        await _context.SaveChangesAsync();
        return targetAuthor;


    }
}