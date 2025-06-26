using LibraryAPI.Contracts.Response;

namespace libraryAPI.Contracts.Response;

public record AuthorResponse(
    Guid Id,
    string? Name,
    List<BookResponse> Books,
    string? CreatedAt,
    string? UpdatedAt,
    string? DeletedAt
);