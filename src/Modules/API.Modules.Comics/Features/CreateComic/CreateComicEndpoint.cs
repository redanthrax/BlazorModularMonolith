using API.Modules.Comics.Entities;
using API.Modules.Comics.Infrastructure.Persistence;
using IntegrationEvents.Comics;
using Common.Application.Models;
using Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Wolverine;

namespace API.Modules.Comics.Features.CreateComic;

public class CreateComicEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/comics",
            async ([FromBody] CreateComicRequest? request,
                ComicsDbContext dbContext,
                IMessageBus messageBus) =>
            {
                if (request == null)
                {
                    return Results.BadRequest(Result<CreateComicResponse>.Failure("Request body is required"));
                }

                var comic = new Comic
                {
                    Title = request.Title,
                    IssueNumber = request.IssueNumber,
                    Publisher = request.Publisher,
                    ReleaseDate = request.ReleaseDate,
                    Description = request.Description,
                    Price = request.Price,
                    CoverImageUrl = request.CoverImageUrl,
                    BoxId = request.BoxId
                };

                dbContext.Comics.Add(comic);
                await dbContext.SaveChangesAsync();

                var integrationEvent = new ComicCreatedIntegrationEvent(
                    Guid.NewGuid(),
                    DateTime.UtcNow,
                    comic.Id,
                    comic.Title,
                    comic.IssueNumber,
                    comic.Publisher,
                    comic.BoxId
                );

                await messageBus.PublishAsync(integrationEvent);
                var response = new CreateComicResponse(
                    comic.Id,
                    comic.Title,
                    comic.IssueNumber,
                    comic.Publisher,
                    comic.ReleaseDate,
                    comic.Description,
                    comic.Price,
                    comic.CoverImageUrl,
                    comic.BoxId,
                    comic.CreatedAt
                );

                return Results.Created($"/api/comics/{comic.Id}", Result<CreateComicResponse>.Success(response));
            })
          .WithName("CreateComic")
          .WithTags("Comics")
          .RequireAuthorization();
    }
}
