namespace API.Modules.Comics.Features.UpdateComic;

public record UpdateComicRequest(
    string Title,
    int IssueNumber,
    string Publisher,
    DateTime? ReleaseDate = null,
    string? Description = null,
    decimal? Price = null,
    string? CoverImageUrl = null,
    Guid? BoxId = null
);
