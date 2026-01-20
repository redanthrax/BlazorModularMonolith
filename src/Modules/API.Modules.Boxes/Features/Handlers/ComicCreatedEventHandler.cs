using IntegrationEvents.Comics;
using Microsoft.Extensions.Logging;

namespace API.Modules.Boxes.Features.Handlers;

public class ComicCreatedEventHandler {
    private readonly ILogger<ComicCreatedEventHandler> _logger;

    public ComicCreatedEventHandler(ILogger<ComicCreatedEventHandler> logger) {
        _logger = logger;
    }

    public void Handle(ComicCreatedIntegrationEvent @event) {
        _logger.LogInformation(
            "Comic created: {ComicId} - {Title}",
            @event.ComicId,
            @event.Title);

        if (@event.BoxId.HasValue) {
            _logger.LogInformation(
                "Comic associated with Box: {BoxId}",
                @event.BoxId);
        }
    }
}
