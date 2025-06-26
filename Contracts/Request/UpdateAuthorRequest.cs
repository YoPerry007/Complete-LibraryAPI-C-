using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Contracts.Request;

public class UpdateAuthorRequest
{
    [Required]
    public string? Name { get; set; }

}