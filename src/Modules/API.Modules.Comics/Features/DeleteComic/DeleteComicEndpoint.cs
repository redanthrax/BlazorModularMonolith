using API.Modules.Comics.Infrastructure.Persistence;
using Common.Application.Models;
using Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace API.Modules.Comics.Features.DeleteComic;

public class DeleteComicEndpoint : IEndpoint {
    public void MapEndpoint(IEndpointRouteBuilder app) {
        app.MapDelete("/api/comics/{id}",
            async (Guid id, ComicsDbContext dbContext) => {
                var comic = await dbContext.Comics.FindAsync(id);
                if (comic == null) {
                    return Results.NotFound(Result<bool>.Failure("Comic not found"));
                }

                dbContext.Comics.Remove(comic);
                await dbContext.SaveChangesAsync();

                return Results.Ok(Result<bool>.Success(true));
            })
            .WithName("DeleteComic")
            .WithTags("Comics")
            .RequireAuthorization();
    }
}
