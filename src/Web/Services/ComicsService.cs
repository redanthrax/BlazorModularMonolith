using System.Net.Http.Json;
using Web.Models;

namespace Web.Services;

public class ComicsService {
    private readonly HttpClient _httpClient;

    public ComicsService(HttpClient httpClient) {
        _httpClient = httpClient;
    }

    public async Task<Result<T>?> CreateComicAsync<T>(CreateComicRequest request) {
        try {
            var response = await _httpClient.PostAsJsonAsync("/api/comics", request);
            
            if (response.IsSuccessStatusCode) {
                return await response.Content.ReadFromJsonAsync<Result<T>>();
            }
            
            return new Result<T> {
                IsSuccess = false,
                Error = $"Error: {response.StatusCode}"
            };
        } catch (Exception ex) {
            return new Result<T> {
                IsSuccess = false,
                Error = ex.Message
            };
        }
    }
}
