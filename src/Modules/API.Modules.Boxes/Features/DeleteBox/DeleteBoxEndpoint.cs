using API.Modules.Boxes.Infrastructure.Persistence;
using Common.Application.Models;
using Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace API.Modules.Boxes.Features.DeleteBox;

public class DeleteBoxEndpoint : IEndpoint {
    public void MapEndpoint(IEndpointRouteBuilder app) {
        app.MapDelete("/api/boxes/{id}",
            async (Guid id, BoxesDbContext dbContext) => {
                var box = await dbContext.Boxes.FindAsync(id);
                if (box == null) {
                    return Results.NotFound(Result<bool>.Failure("Box not found"));
                }

                dbContext.Boxes.Remove(box);
                await dbContext.SaveChangesAsync();

                return Results.Ok(Result<bool>.Success(true));
            })
            .WithName("DeleteBox")
            .WithTags("Boxes")
            .RequireAuthorization();
    }
}
