namespace Dishes.Application.Abstractions.Errors;

public sealed class SuccessResponse() : Response(true);

public sealed class SuccessResponse<T>(T data) : Response<T>(true, data);