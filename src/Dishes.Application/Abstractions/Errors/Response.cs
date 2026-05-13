namespace Dishes.Application.Abstractions.Errors;

public abstract class Response(bool isSuccess, Error? error = null)
{
    public bool IsSuccess { get; protected init; } = isSuccess;
    public Error? Error { get; protected init; } = error;

    public static Response Success() => new SuccessResponse();
    public static Response Failure(Error error) => new FailureResponse(error);
}

public abstract class Response<T>(bool isSuccess, T? data = default, Error? error = null) : Response(isSuccess, error)
{
    public T? Data { get; } = data;

    public static Response<T> Success(T data) => new SuccessResponse<T>(data);
}