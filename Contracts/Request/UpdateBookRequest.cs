using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Contracts.Request;

public class UpdateBookRequest
{
    [Required]
    public string? Title { get; set; }
    [Required]
    public string? AuthorId { get; set; }

}