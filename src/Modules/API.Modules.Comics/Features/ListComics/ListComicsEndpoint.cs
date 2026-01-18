using API.Modules.Comics.Infrastructure.Persistence;
using Common.Application.Models;
using Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace API.Modules.Comics.Features.ListComics;

public class ListComicsEndpoint : IEndpoint {
    public void MapEndpoint(IEndpointRouteBuilder app) {
        app.MapGet("/api/comics",
            async (ComicsDbContext dbContext, int page = 1, int pageSize = 10) => {
                var pagedRequest = new PagedRequest(page, pageSize);

                var totalCount = await dbContext.Comics.CountAsync();

                var comics = await dbContext.Comics
                    .OrderBy(c => c.Title)
                    .Skip(pagedRequest.Skip)
                    .Take(pagedRequest.Take)
                    .Select(c => new ComicListItemResponse(
                        c.Id,
                        c.Title,
                        c.IssueNumber,
                        c.Publisher,
                        c.ReleaseDate,
                        c.Price,
                        c.BoxId
                    ))
                    .ToListAsync();

                return Results.Ok(PagedResult<ComicListItemResponse>.Success(comics, totalCount, page,
                    pageSize));
            })
            .WithName("ListComics")
            .WithTags("Comics")
            .RequireAuthorization();
    }
}
