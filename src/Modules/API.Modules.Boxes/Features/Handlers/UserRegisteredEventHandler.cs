using IntegrationEvents.Authentication;
using Microsoft.Extensions.Logging;

namespace API.Modules.Boxes.Features.Handlers;

public class UserRegisteredEventHandler {
    private readonly ILogger<UserRegisteredEventHandler> _logger;

    public UserRegisteredEventHandler(ILogger<UserRegisteredEventHandler> logger) {
        _logger = logger;
    }

    public void Handle(UserRegisteredIntegrationEvent @event) {
        _logger.LogInformation(
            "User registered: {Email}",
            @event.Email);
    }
}
