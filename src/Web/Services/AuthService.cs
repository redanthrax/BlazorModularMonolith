using System.Net.Http.Json;
using Web.Models;

namespace Web.Services;

public class AuthService {
    private readonly HttpClient _httpClient;
    private readonly CustomAuthenticationStateProvider _authStateProvider;

    public AuthService(HttpClient httpClient, CustomAuthenticationStateProvider authStateProvider) {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
    }

    public async Task<Result<LoginResponse>?> LoginAsync(LoginRequest request) {
        try {
            var response = await _httpClient.PostAsJsonAsync("/api/auth/login", request);

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<Result<LoginResponse>>();
                
                if (result?.IsSuccess == true && result.Data != null) {
                    await _authStateProvider.MarkUserAsAuthenticated(result.Data.Token);
                }

                return result;
            }

            var errorResult = await response.Content.ReadFromJsonAsync<Result<LoginResponse>>();
            return errorResult ?? new Result<LoginResponse> {
                IsSuccess = false,
                Error = $"Error: {response.StatusCode}"
            };
        } catch (Exception ex) {
            return new Result<LoginResponse> {
                IsSuccess = false,
                Error = ex.Message
            };
        }
    }

    public async Task<Result<LoginResponse>?> RegisterAsync(RegisterRequest request) {
        try {
            var response = await _httpClient.PostAsJsonAsync("/api/auth/register", request);

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<Result<LoginResponse>>();
                
                if (result?.IsSuccess == true && result.Data != null) {
                    await _authStateProvider.MarkUserAsAuthenticated(result.Data.Token);
                }

                return result;
            }

            var errorResult = await response.Content.ReadFromJsonAsync<Result<LoginResponse>>();
            return errorResult ?? new Result<LoginResponse> {
                IsSuccess = false,
                Error = $"Error: {response.StatusCode}"
            };
        } catch (Exception ex) {
            return new Result<LoginResponse> {
                IsSuccess = false,
                Error = ex.Message
            };
        }
    }

    public async Task LogoutAsync() {
        await _authStateProvider.MarkUserAsLoggedOut();
    }
}
