using API.Modules.Comics.Infrastructure.Persistence;
using Common.Application.Models;
using Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace API.Modules.Comics.Features.GetComic;

public class GetComicEndpoint : IEndpoint {
    public void MapEndpoint(IEndpointRouteBuilder app) {
        app.MapGet("/api/comics/{id}",
            async (Guid id, ComicsDbContext dbContext) => {
                var comic = await dbContext.Comics.FindAsync(id);
                if (comic == null) {
                    return Results.NotFound(Result<GetComicResponse>.Failure("Comic not found"));
                }

                var response = new GetComicResponse(
                    comic.Id,
                    comic.Title,
                    comic.IssueNumber,
                    comic.Publisher,
                    comic.ReleaseDate,
                    comic.Description,
                    comic.Price,
                    comic.CoverImageUrl,
                    comic.BoxId,
                    comic.CreatedAt,
                    comic.UpdatedAt
                );

                return Results.Ok(Result<GetComicResponse>.Success(response));
            })
            .WithName("GetComic")
            .WithTags("Comics")
            .RequireAuthorization();
    }
}
