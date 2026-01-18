using Common.Application.Messaging;

namespace API.Modules.Authentication.IntegrationEvents;

public sealed record UserRegisteredIntegrationEvent(
    Guid Id,
    DateTime OccurredOnUtc,
    Guid UserId,
    string Email,
    string FullName) : IIntegrationEvent {
    public UserRegisteredIntegrationEvent(Guid UserId, string Email, string FullName)
        : this(Guid.NewGuid(), DateTime.UtcNow, UserId, Email, FullName) {
    }
}
