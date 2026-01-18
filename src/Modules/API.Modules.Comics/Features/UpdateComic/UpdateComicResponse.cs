namespace API.Modules.Comics.Features.UpdateComic;

public record UpdateComicResponse(
    Guid Id,
    string Title,
    int IssueNumber,
    string Publisher,
    DateTime? ReleaseDate,
    string? Description,
    decimal? Price,
    string? CoverImageUrl,
    Guid? BoxId,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
