using Common.Application.Messaging;

namespace API.Modules.Boxes.IntegrationEvents;

public sealed record BoxCreatedIntegrationEvent(
    Guid Id,
    DateTime OccurredOnUtc,
    Guid BoxId,
    string Name,
    string? Location,
    int? Capacity) : IIntegrationEvent {
    public BoxCreatedIntegrationEvent(Guid BoxId, string Name, string? Location, int? Capacity)
        : this(Guid.NewGuid(), DateTime.UtcNow, BoxId, Name, Location, Capacity) {
    }
}
