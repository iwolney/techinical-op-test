namespace TechnicalTestOpea.Ports.OperationAPI.Models;

public sealed class ApiResponse<T>
{
    public bool Success { get; init; }
    public T? Data { get; init; }
    public IReadOnlyCollection<string> Errors { get; init; } = [];

    public static ApiResponse<T> Ok(T data)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = data
        };
    }

    public static ApiResponse<T> Fail(params string[] errors)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Errors = errors
        };
    }
}