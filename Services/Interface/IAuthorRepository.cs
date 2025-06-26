using LibraryAPI.Models;

namespace libraryAPI.Services.Interface;

public interface IAuthorRepository
{
    Task<Author?> Create(Author author);
    Task<IEnumerable<Author>> GetAll();
    Task<Author?> GetById(Guid id);
    Task<Author?> UpdateById(Guid id, Author author);
    Task<bool?> DeleteById(Guid id);
    Task GetById(Guid? authorId);
    Task GetById(string? authorId);
}