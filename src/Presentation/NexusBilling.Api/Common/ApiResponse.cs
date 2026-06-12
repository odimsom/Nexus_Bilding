namespace NexusBilling.Api.Common;

public record ApiResponse<T>(bool Success, T? Data, ApiError? Error, ApiMeta Meta)
{
    public static ApiResponse<T> Ok(T data) =>
        new(true, data, null, ApiMeta.Now());

    public static ApiResponse<object?> Fail(string code, string message, IReadOnlyList<FieldError>? details = null) =>
        new(false, default, new ApiError(code, message, details ?? []), ApiMeta.Now());

    public static ApiResponse<object?> NotFound(string message = "El recurso no fue encontrado.") =>
        Fail("NOT_FOUND", message);
}

public record ApiError(string Code, string Message, IReadOnlyList<FieldError> Details);
public record FieldError(string Field, string Message);
public record ApiMeta(string RequestId, DateTimeOffset Timestamp)
{
    public static ApiMeta Now() => new(Guid.NewGuid().ToString("N")[..8], DateTimeOffset.UtcNow);
}

public record PagedResult<T>(IReadOnlyList<T> Items, PaginationMeta Pagination);
public record PaginationMeta(int Page, int PageSize, int TotalItems, int TotalPages);
