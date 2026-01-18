namespace API.Modules.Boxes.Features.GetBox;

public record GetBoxResponse(
    Guid Id,
    string Name,
    string? Location,
    int? Capacity,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
