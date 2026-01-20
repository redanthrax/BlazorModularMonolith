using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Web.Services;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider {
    private readonly IJSRuntime _jsRuntime;
    private const string TokenKey = "authToken";
    private ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public CustomAuthenticationStateProvider(IJSRuntime jsRuntime) {
        _jsRuntime = jsRuntime;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
        try {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);

            if (string.IsNullOrWhiteSpace(token)) {
                return new AuthenticationState(_anonymous);
            }

            var claims = ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        } catch {
            return new AuthenticationState(_anonymous);
        }
    }

    public async Task MarkUserAsAuthenticated(string token) {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, token);

        var claims = ParseClaimsFromJwt(token);
        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task MarkUserAsLoggedOut() {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);

        var anonymousUser = _anonymous;
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
    }

    public async Task<string?> GetTokenAsync() {
        try {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);
        } catch {
            return null;
        }
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt) {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        if (keyValuePairs != null) {
            keyValuePairs.TryGetValue(ClaimTypes.Role, out var roles);

            if (roles != null) {
                var rolesString = roles.ToString();
                if (rolesString?.Trim().StartsWith("[") == true) {
                    var parsedRoles = System.Text.Json.JsonSerializer.Deserialize<string[]>(rolesString);
                    if (parsedRoles != null) {
                        claims.AddRange(parsedRoles.Select(role => new Claim(ClaimTypes.Role, role)));
                    }
                } else if (!string.IsNullOrEmpty(rolesString)) {
                    claims.Add(new Claim(ClaimTypes.Role, rolesString));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value?.ToString() ?? "")));
        }

        return claims;
    }

    private static byte[] ParseBase64WithoutPadding(string base64) {
        switch (base64.Length % 4) {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}
