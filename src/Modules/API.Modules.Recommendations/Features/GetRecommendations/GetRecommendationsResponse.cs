namespace API.Modules.Recommendations.Features.GetRecommendations;

public record RecommendationItemResponse(
    Guid RecommendedComicId,
    string Reason,
    double Score
);

public record GetRecommendationsResponse(
    List<RecommendationItemResponse> Recommendations
);
