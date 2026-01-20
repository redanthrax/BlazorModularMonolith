using IntegrationEvents.Boxes;
using Microsoft.Extensions.Logging;

namespace API.Modules.Comics.Features.Handlers;

public class BoxCreatedEventHandler {
    private readonly ILogger<BoxCreatedEventHandler> _logger;

    public BoxCreatedEventHandler(ILogger<BoxCreatedEventHandler> logger) {
        _logger = logger;
    }

    public void Handle(BoxCreatedIntegrationEvent @event) {
        _logger.LogInformation(
            "Box created: {BoxId} - {Name}",
            @event.BoxId,
            @event.Name);

        if (@event.Capacity.HasValue) {
            _logger.LogInformation(
                "Box capacity: {Capacity}",
                @event.Capacity);
        }
    }
}
