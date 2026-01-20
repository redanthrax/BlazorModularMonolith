using API.Modules.Recommendations.Infrastructure.Persistence;
using Common.Application.Models;
using Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace API.Modules.Recommendations.Features.GetRecommendations;

public class GetRecommendationsEndpoint : IEndpoint {
    public void MapEndpoint(IEndpointRouteBuilder app) {
        app.MapGet("/api/comics/{comicId}/recommendations",
            async (Guid comicId, RecommendationsDbContext dbContext) => {
                var recommendations = await dbContext.Recommendations
                    .Where(r => r.SourceComicId == comicId)
                    .OrderByDescending(r => r.Score)
                    .Select(r => new RecommendationItemResponse(
                        r.RecommendedComicId,
                        r.Reason,
                        r.Score
                    ))
                    .ToListAsync();

                var response = new GetRecommendationsResponse(recommendations);
                return Results.Ok(Result<GetRecommendationsResponse>.Success(response));
            })
            .WithName("GetRecommendations")
            .WithTags("Recommendations")
            .RequireAuthorization();
    }
}
