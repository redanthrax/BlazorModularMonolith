namespace API.Modules.Comics.Features.GetComic;

public record GetComicResponse(
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
