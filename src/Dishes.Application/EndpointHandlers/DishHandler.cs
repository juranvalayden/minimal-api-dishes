using Dishes.Application.Abstractions.Errors;
using Dishes.Application.Dtos;
using Dishes.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Dishes.Application.EndpointHandlers;

public static class DishHandler
{
    public static async Task<Results<BadRequest, Ok<IEnumerable<DishDto>>>> GetDishesAsync(IDishService dishService, [FromQuery] string? name, CancellationToken cancellationToken)
    {
        var response = await dishService.GetDishesAsync(name, false, cancellationToken);

        if (response is Response<IEnumerable<DishDto>> success)
        {
            return TypedResults.Ok(success.Data);
        }

        return TypedResults.BadRequest();
    }

    public static async Task<Results<NotFound, BadRequest, Ok<DishDto>>> GetDishByIdAsync(IDishService dishService, Guid dishId, CancellationToken cancellationToken)
    {
        var response = await dishService.GetDishByIdAsync(dishId, false, cancellationToken);

        if (response is Response<DishDto> success)
        {
            return TypedResults.Ok(success.Data);
        }

        if (response.Error!.ErrorType == ErrorType.NotFound)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.BadRequest();
    }

    //public static async Task<IResult> GetDishByNameAsync(IDishService dishService, string dishName, CancellationToken cancellationToken)
    //{
    //    var response = await dishService.GetDishByNameAsync(dishName, true, cancellationToken);

    //    return ResponseHandler<DishDto>.HandleResponse(response);
    //}

    //public static async Task<Results<NotFound, BadRequest, Ok<DishDto>>> GetDishByNameAsync(IDishService dishService, string dishName, CancellationToken cancellationToken)
    //{
    //    var response = await dishService.GetDishByNameAsync(dishName, true, cancellationToken);

    //    return ResponseHandler<DishDto>.HandleResponse(response);
    //}

    public static async Task<IResult> GetDishByNameAsync(IDishService dishService, string dishName, CancellationToken cancellationToken)
    {
        var response = await dishService.GetDishByNameAsync(dishName, true, cancellationToken);

        return ResponseHandler.HandleResponse<DishDto>(response);
    }
}