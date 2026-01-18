using API.Modules.Boxes.Infrastructure.Persistence;
using Common.Application.Models;
using Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace API.Modules.Boxes.Features.GetBox;

public class GetBoxEndpoint : IEndpoint {
    public void MapEndpoint(IEndpointRouteBuilder app) {
        app.MapGet("/api/boxes/{id}",
            async (Guid id, BoxesDbContext dbContext) => {
                var box = await dbContext.Boxes.FindAsync(id);
                if (box == null) {
                    return Results.NotFound(Result<GetBoxResponse>.Failure("Box not found"));
                }

                var response = new GetBoxResponse(
                    box.Id,
                    box.Name,
                    box.Location,
                    box.Capacity,
                    box.Description,
                    box.CreatedAt,
                    box.UpdatedAt
                );

                return Results.Ok(Result<GetBoxResponse>.Success(response));
            })
            .WithName("GetBox")
            .WithTags("Boxes")
            .RequireAuthorization();
    }
}
