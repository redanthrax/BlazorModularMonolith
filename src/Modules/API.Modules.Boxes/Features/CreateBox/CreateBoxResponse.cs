namespace API.Modules.Boxes.Features.CreateBox;

public record CreateBoxResponse(
    Guid Id,
    string Name,
    string? Location,
    int? Capacity,
    string? Description,
    DateTime CreatedAt
);
