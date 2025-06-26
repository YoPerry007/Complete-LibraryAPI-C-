namespace LibraryAPI.Contracts.Response;

public record BookResponse(
    Guid Id,
    string? Title,
    Guid? AuthorId,
    string? AuthorName,
    string? CreatedAt,
    string? UpdatedAt,
    string? DeletedAt
);