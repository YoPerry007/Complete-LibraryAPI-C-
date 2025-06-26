using libraryAPI.Models;
using libraryAPI.Services.Interface;
using LibraryAPI.AppDataContext;
using Microsoft.EntityFrameworkCore;

namespace libraryAPI.Services.Repository;

public class BookRepository : IBookRepository
{
    private readonly LibraryDBContext libraryDBContext;

    public BookRepository(LibraryDBContext libraryDBContext)
    {
        this.libraryDBContext = libraryDBContext;
    }

    public async Task<Book?> Create(Book book)
    {
        var newBook = new Book
        {
            Title = book.Title,
            AuthorId = book.AuthorId,
            CreatedAt = DateTime.UtcNow.ToString(),

        };
        libraryDBContext.Books.Add(newBook);
        await libraryDBContext.SaveChangesAsync();
        return newBook;
    }

    public async Task<bool?> DeleteById(Guid id)
    {
        var targetBook = await libraryDBContext.Books.FirstOrDefaultAsync(tB => tB.Id == id);
        if (targetBook == null)
        {
            return false;
        }
        libraryDBContext.Books.Remove(targetBook);
        await libraryDBContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Book>?> GetByAuthorId(Guid id)
    {
        var tBook = await libraryDBContext.Books.Where(tB => tB.DeletedAt == null && tB.AuthorId == id).ToListAsync();
        if (tBook == null)
        {
            return null;
        }
        return tBook;
    }
    public async Task<IEnumerable<Book>> GetAll()
    {
        var allbooks = await libraryDBContext.Books.Where(tB => tB.DeletedAt == null).Include(b => b.Author).ToListAsync();
        return allbooks;
    }

    public async Task<Book?> GetById(Guid id)
    {
        var tBook = await libraryDBContext.Books.FirstOrDefaultAsync(tB => tB.Id == id);
        if (tBook == null)
        {
            return null;
        }
        return tBook;
    }

    public async Task<Book?> UpdateById(Guid id, Book book)
    {
        var tBook = await libraryDBContext.Books.FirstOrDefaultAsync(tB => tB.Id == id);
        if (tBook == null)
        {
            return null;
        }
        tBook.Title = book.Title;
        tBook.AuthorId = book.AuthorId;
        tBook.UpdatedAt = DateTime.UtcNow.ToString();
        libraryDBContext.Update(tBook);
        await libraryDBContext.SaveChangesAsync();
        return tBook;
    }

    public Task GetByAuthorId()
    {
        throw new NotImplementedException();
    }
}