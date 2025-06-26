using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Contracts.Request;

public class CreateBookRequest
{
    [Required]
    public string? Title { get; set; }
    public Guid AuthorId { get; set; }
}