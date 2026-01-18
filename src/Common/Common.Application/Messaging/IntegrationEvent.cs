namespace Common.Application.Messaging;

public abstract record IntegrationEvent(Guid Id, DateTime OccurredOnUtc) : IIntegrationEvent {
    protected IntegrationEvent() : this(Guid.NewGuid(), DateTime.UtcNow) {
    }
}
