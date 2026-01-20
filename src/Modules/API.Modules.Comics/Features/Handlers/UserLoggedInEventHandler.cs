using IntegrationEvents.Authentication;
using Microsoft.Extensions.Logging;

namespace API.Modules.Comics.Features.Handlers;

public class UserLoggedInEventHandler {
    private readonly ILogger<UserLoggedInEventHandler> _logger;

    public UserLoggedInEventHandler(ILogger<UserLoggedInEventHandler> logger) {
        _logger = logger;
    }

    public void Handle(UserLoggedInIntegrationEvent @event) {
        _logger.LogInformation(
            "User logged in: {Email}",
            @event.Email);
    }
}
