using System.Net.Http.Headers;

namespace Web.Services;

public class AuthorizationMessageHandler : DelegatingHandler {
    private readonly CustomAuthenticationStateProvider _authStateProvider;

    public AuthorizationMessageHandler(CustomAuthenticationStateProvider authStateProvider) {
        _authStateProvider = authStateProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken) {
        var token = await _authStateProvider.GetTokenAsync();

        if (!string.IsNullOrWhiteSpace(token)) {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
