using Common.Application.Messaging;

namespace API.Modules.Comics.IntegrationEvents;

public sealed record ComicCreatedIntegrationEvent(
    Guid Id,
    DateTime OccurredOnUtc,
    Guid ComicId,
    string Title,
    int IssueNumber,
    string Publisher,
    Guid? BoxId) : IIntegrationEvent {
    public ComicCreatedIntegrationEvent(Guid ComicId, string Title, int IssueNumber, string Publisher,
        Guid? BoxId)
        : this(Guid.NewGuid(), DateTime.UtcNow, ComicId, Title, IssueNumber, Publisher, BoxId) {
    }
}
