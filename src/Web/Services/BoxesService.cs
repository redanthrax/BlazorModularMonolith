using System.Net.Http.Json;
using Web.Models;

namespace Web.Services;

public class BoxesService {
    private readonly HttpClient _httpClient;

    public BoxesService(HttpClient httpClient) {
        _httpClient = httpClient;
    }

    public async Task<PagedResult<BoxListItemResponse>?> GetBoxesAsync(int page = 1, int pageSize = 100) {
        try {
            var response = await _httpClient.GetFromJsonAsync<PagedResult<BoxListItemResponse>>(
                $"/api/boxes?page={page}&pageSize={pageSize}");
            return response;
        } catch (Exception ex) {
            return new PagedResult<BoxListItemResponse> {
                IsSuccess = false,
                Error = ex.Message
            };
        }
    }
}
