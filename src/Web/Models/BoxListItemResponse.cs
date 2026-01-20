namespace Web.Models;

public class BoxListItemResponse {
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }
    public int? Capacity { get; set; }
}
