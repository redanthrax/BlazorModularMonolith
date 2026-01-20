using Common.Domain;

namespace API.Modules.Recommendations.Entities;

public class Recommendation : BaseEntity {
    public Guid SourceComicId { get; set; }
    public Guid RecommendedComicId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public double Score { get; set; }
}
