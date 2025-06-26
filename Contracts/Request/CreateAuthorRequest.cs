using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Contracts.Request;

public class CreateAuthorRequest
{
    [Required]
    public string? Name { get; set; }

}