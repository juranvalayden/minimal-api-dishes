namespace Dishes.Application.Abstractions.Errors;

public sealed class FailureResponse(Error error) : Response(false, error);