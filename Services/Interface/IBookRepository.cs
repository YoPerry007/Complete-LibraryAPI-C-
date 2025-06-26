using libraryAPI.Models;

namespace libraryAPI.Services.Interface;

public interface IBookRepository
{
    Task<Book?> Create(Book book);
    Task<IEnumerable<Book>> GetAll();
    Task<Book?> GetById(Guid id);
    Task<Book?> UpdateById(Guid id, Book book);
    Task<bool?> DeleteById(Guid id);
    Task<IEnumerable<Book>?> GetByAuthorId(Guid authorId);
    Task GetByAuthorId();
}