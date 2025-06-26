namespace libraryAPI.Models.Common;

public class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? CreatedAt { get; set; } 
    public string? UpdatedAt { get; set; }
    public string? DeletedAt { get; set; }
    }
