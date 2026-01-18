using API.Modules.Boxes.Infrastructure.Persistence;
using Common.Application.Models;
using Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace API.Modules.Boxes.Features.UpdateBox;

public class UpdateBoxEndpoint : IEndpoint {
    public void MapEndpoint(IEndpointRouteBuilder app) {
        app.MapPut("/api/boxes/{id}",
            async (Guid id, [FromBody] UpdateBoxRequest request, BoxesDbContext dbContext) => {
                var box = await dbContext.Boxes.FindAsync(id);
                if (box == null) {
                    return Results.NotFound(Result<UpdateBoxResponse>.Failure("Box not found"));
                }

                box.Name = request.Name;
                box.Location = request.Location;
                box.Capacity = request.Capacity;
                box.Description = request.Description;
                box.UpdatedAt = DateTime.UtcNow;

                await dbContext.SaveChangesAsync();

                var response = new UpdateBoxResponse(
                    box.Id,
                    box.Name,
                    box.Location,
                    box.Capacity,
                    box.Description,
                    box.CreatedAt,
                    box.UpdatedAt
                );

                return Results.Ok(Result<UpdateBoxResponse>.Success(response));
            })
            .WithName("UpdateBox")
            .WithTags("Boxes")
            .RequireAuthorization();
    }
}
