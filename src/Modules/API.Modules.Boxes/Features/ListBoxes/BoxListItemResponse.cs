namespace API.Modules.Boxes.Features.ListBoxes;

public record BoxListItemResponse(
    Guid Id,
    string Name,
    string? Location,
    int? Capacity
);
