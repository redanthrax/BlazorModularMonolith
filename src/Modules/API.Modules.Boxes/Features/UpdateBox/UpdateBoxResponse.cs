namespace API.Modules.Boxes.Features.UpdateBox;

public record UpdateBoxResponse(
    Guid Id,
    string Name,
    string? Location,
    int? Capacity,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
