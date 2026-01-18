namespace API.Modules.Boxes.Features.CreateBox;

public record CreateBoxRequest(
    string Name,
    string? Location = null,
    int? Capacity = null,
    string? Description = null
);
