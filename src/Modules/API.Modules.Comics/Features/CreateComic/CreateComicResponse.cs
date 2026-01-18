namespace API.Modules.Comics.Features.CreateComic;

public record CreateComicResponse(
    Guid Id,
    string Title,
    int IssueNumber,
    string Publisher,
    DateTime? ReleaseDate,
    string? Description,
    decimal? Price,
    string? CoverImageUrl,
    Guid? BoxId,
    DateTime CreatedAt
);
