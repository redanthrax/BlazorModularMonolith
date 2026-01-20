using IntegrationEvents.Comics;
using API.Modules.Recommendations.Entities;
using API.Modules.Recommendations.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Modules.Recommendations.Features.Handlers;

public class ComicCreatedEventHandler {
    private readonly RecommendationsDbContext _dbContext;
    private readonly ILogger<ComicCreatedEventHandler> _logger;

    public ComicCreatedEventHandler(RecommendationsDbContext dbContext, ILogger<ComicCreatedEventHandler> logger) {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Handle(ComicCreatedIntegrationEvent @event) {
        _logger.LogInformation("Recommendations handler called for comic: {ComicId} - {Title} by {Publisher}",
            @event.ComicId, @event.Title, @event.Publisher);

        var existingComics = await _dbContext.Recommendations
            .Select(r => new { r.SourceComicId, r.RecommendedComicId })
            .ToListAsync();

        var existingComicIds = existingComics
            .SelectMany(r => new[] { r.SourceComicId, r.RecommendedComicId })
            .Distinct()
            .ToList();

        var recommendations = new List<Recommendation>();

        foreach (var existingComicId in existingComicIds) {
            var samePublisherScore = 75.0;
            var recommendation = new Recommendation {
                SourceComicId = @event.ComicId,
                RecommendedComicId = existingComicId,
                Reason = $"Same publisher: {@event.Publisher}",
                Score = samePublisherScore
            };
            recommendations.Add(recommendation);

            var reverseRecommendation = new Recommendation {
                SourceComicId = existingComicId,
                RecommendedComicId = @event.ComicId,
                Reason = $"Same publisher: {@event.Publisher}",
                Score = samePublisherScore
            };
            recommendations.Add(reverseRecommendation);
        }

        if (recommendations.Any()) {
            await _dbContext.Recommendations.AddRangeAsync(recommendations);
            await _dbContext.SaveChangesAsync();
        }
    }
}
