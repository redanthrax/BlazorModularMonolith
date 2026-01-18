using Common.Domain;

namespace API.Modules.Boxes.Entities;

public class Box : BaseEntity {
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }
    public int? Capacity { get; set; }
    public string? Description { get; set; }
}
