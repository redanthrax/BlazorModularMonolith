namespace API.Modules.Boxes.Features.UpdateBox;

public record UpdateBoxRequest(
    string Name,
    string? Location = null,
    int? Capacity = null,
    string? Description = null
);
