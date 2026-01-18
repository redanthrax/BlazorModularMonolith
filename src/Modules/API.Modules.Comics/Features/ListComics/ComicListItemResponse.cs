namespace API.Modules.Comics.Features.ListComics;

public record ComicListItemResponse(
    Guid Id,
    string Title,
    int IssueNumber,
    string Publisher,
    DateTime? ReleaseDate,
    decimal? Price,
    Guid? BoxId
);
