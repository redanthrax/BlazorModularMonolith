using Common.Application.Messaging;

namespace API.Modules.Authentication.IntegrationEvents;

public sealed record UserRegisteredIntegrationEvent(
    Guid Id,
    DateTime OccurredOnUtc,
    Guid UserId,
    string Email) : IIntegrationEvent {
    public UserRegisteredIntegrationEvent(Guid UserId, string Email)
        : this(Guid.NewGuid(), DateTime.UtcNow, UserId, Email) {
    }
}
