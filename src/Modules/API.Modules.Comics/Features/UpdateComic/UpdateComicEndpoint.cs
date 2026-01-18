using API.Modules.Comics.Infrastructure.Persistence;
using Common.Application.Models;
using Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace API.Modules.Comics.Features.UpdateComic;

public class UpdateComicEndpoint : IEndpoint {
    public void MapEndpoint(IEndpointRouteBuilder app) {
        app.MapPut("/api/comics/{id}",
            async (Guid id, [FromBody] UpdateComicRequest request, ComicsDbContext dbContext) => {
                var comic = await dbContext.Comics.FindAsync(id);
                if (comic == null) {
                    return Results.NotFound(Result<UpdateComicResponse>.Failure("Comic not found"));
                }

                comic.Title = request.Title;
                comic.IssueNumber = request.IssueNumber;
                comic.Publisher = request.Publisher;
                comic.ReleaseDate = request.ReleaseDate;
                comic.Description = request.Description;
                comic.Price = request.Price;
                comic.CoverImageUrl = request.CoverImageUrl;
                comic.BoxId = request.BoxId;
                comic.UpdatedAt = DateTime.UtcNow;

                await dbContext.SaveChangesAsync();

                var response = new UpdateComicResponse(
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

                return Results.Ok(Result<UpdateComicResponse>.Success(response));
            })
            .WithName("UpdateComic")
            .WithTags("Comics")
            .RequireAuthorization();
    }
}
