namespace Common.Application.Models;

public class PagedResult<T> {
    public bool IsSuccess { get; init; }
    public List<T>? Data { get; init; }
    public int TotalCount { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
    public bool HasNextPage => Page < TotalPages;
    public bool HasPreviousPage => Page > 1;
    public string? Error { get; init; }
    public List<string>? Errors { get; init; }

    public static PagedResult<T> Success(List<T> data, int totalCount, int page, int pageSize) {
        return new PagedResult<T> {
            IsSuccess = true,
            Data = data,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public static PagedResult<T> Failure(string error) {
        return new PagedResult<T> {
            IsSuccess = false,
            Error = error,
            Errors = new List<string> { error }
        };
    }

    public static PagedResult<T> Failure(List<string> errors) {
        return new PagedResult<T> {
            IsSuccess = false,
            Error = errors.FirstOrDefault(),
            Errors = errors
        };
    }
}
