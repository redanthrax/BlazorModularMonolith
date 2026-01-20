using Common.Application.Messaging;

namespace IntegrationEvents.Comics;

public sealed record ComicCreatedIntegrationEvent(
    Guid Id,
    DateTime OccurredOnUtc,
    Guid ComicId,
    string Title,
    int IssueNumber,
    string Publisher,
    Guid? BoxId) : IIntegrationEvent;
