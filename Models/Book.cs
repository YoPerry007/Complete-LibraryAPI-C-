using libraryAPI.Models.Common;
using LibraryAPI.Models;
namespace libraryAPI.Models;

public class Book : Entity
{
    public string? Title { get; set; }
    public Guid? AuthorId { get; set; }
    public Author? Author { get; set; }
}