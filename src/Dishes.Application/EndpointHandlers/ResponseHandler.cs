using Dishes.Application.Abstractions.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Dishes.Application.EndpointHandlers;

public static class ResponseHandler
{
    public static IResult HandleResponse<T>(Response response) where T : class
    {
        if (response is Response<T> success)
        {
            return TypedResults.Ok(success.Data);
        }

        return response.Error!.ErrorType switch
        {
            ErrorType.NotFound => TypedResults.NotFound(),
            _ => TypedResults.BadRequest()
        };
    }
}

public static class ResponseHandler<T>
{
    public static Results<NotFound, BadRequest, Ok<T>> HandleResponse(Response response)
    {
        if (response is Response<T> success)
        {
            return TypedResults.Ok(success.Data);
        }

        return response.Error!.ErrorType switch
        {
            ErrorType.NotFound => TypedResults.NotFound(),
            _ => TypedResults.BadRequest()
        };
    }
}