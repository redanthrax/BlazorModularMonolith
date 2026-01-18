namespace Common.Application.Models;

public class Result<T> {
    public bool IsSuccess { get; init; }
    public T? Data { get; init; }
    public string? Error { get; init; }
    public List<string>? Errors { get; init; }

    public static Result<T> Success(T data) {
        return new Result<T> {
            IsSuccess = true,
            Data = data
        };
    }

    public static Result<T> Failure(string error) {
        return new Result<T> {
            IsSuccess = false,
            Error = error,
            Errors = new List<string> { error }
        };
    }

    public static Result<T> Failure(List<string> errors) {
        return new Result<T> {
            IsSuccess = false,
            Error = errors.FirstOrDefault(),
            Errors = errors
        };
    }
}
