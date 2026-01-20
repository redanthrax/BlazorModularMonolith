using API.Modules.Boxes.Entities;
using API.Modules.Boxes.Infrastructure.Persistence;
using IntegrationEvents.Boxes;
using Common.Application.Models;
using Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Wolverine;

namespace API.Modules.Boxes.Features.CreateBox;

public class CreateBoxEndpoint : IEndpoint {
    public void MapEndpoint(IEndpointRouteBuilder app) {
        app.MapPost("/api/boxes",
            async ([FromBody] CreateBoxRequest? request,
                BoxesDbContext dbContext,
                IMessageBus messageBus) => {
                if (request == null) {
                    return Results.BadRequest(Result<CreateBoxResponse>.Failure("Request body is required"));
                }

                var box = new Box {
                    Name = request.Name,
                    Location = request.Location,
                    Capacity = request.Capacity,
                    Description = request.Description
                };

                dbContext.Boxes.Add(box);
                await dbContext.SaveChangesAsync();

                var integrationEvent = new BoxCreatedIntegrationEvent(
                    box.Id,
                    box.Name,
                    box.Location,
                    box.Capacity
                );
                await messageBus.PublishAsync(integrationEvent);

                var response = new CreateBoxResponse(
                    box.Id,
                    box.Name,
                    box.Location,
                    box.Capacity,
                    box.Description,
                    box.CreatedAt
                );

                return Results.Created($"/api/boxes/{box.Id}", Result<CreateBoxResponse>.Success(response));
            })
            .WithName("CreateBox")
            .WithTags("Boxes")
            .RequireAuthorization();
    }
}
