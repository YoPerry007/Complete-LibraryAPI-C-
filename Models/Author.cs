using libraryAPI.Models;
using libraryAPI.Models.Common;
// using LibraryAPI.Models.Common;
// namespace LibraryAPI.Models.Common;
namespace LibraryAPI.Models;

public class Author : Entity
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public List<Book> Books { get; set; } = new List<Book>();

}