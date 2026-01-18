using Common.Domain;

namespace API.Modules.Comics.Entities;

public class Comic : BaseEntity {
    public string Title { get; set; } = string.Empty;
    public int IssueNumber { get; set; }
    public string Publisher { get; set; } = string.Empty;
    public DateTime? ReleaseDate { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public string? CoverImageUrl { get; set; }
    public Guid? BoxId { get; set; }
}
