using Dishes.Application.Dtos;
using Dishes.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Dishes.Application.EndpointHandlers;

public static class DishesHandler
{
    public static async Task<Ok<IEnumerable<DishesDto>>> GetDishesAsync(IDishService dishService, CancellationToken cancellationToken, [FromQuery] string? name)
    {
        var dishDto = await dishService.GetDishesAsync(name, false, cancellationToken);
        return TypedResults.Ok(dishDto);
    }

    public static async Task<Results<NotFound, Ok<DishesDto>>> GetDishByIdAsync(IDishService dishService, CancellationToken cancellationToken, Guid dishId)
    {
        var dishDto = await dishService.GetDishByIdAsync(dishId, false, cancellationToken);

        if (dishDto is null) return TypedResults.NotFound();

        return TypedResults.Ok(dishDto);
    }

    public static async Task<Results<NotFound, Ok<DishesDto>>> GetDishByNameAsync(IDishService dishService, CancellationToken cancellationToken, string dishName)
    {
        var dishDto = await dishService.GetDishByNameAsync(dishName, true, cancellationToken);

        if (dishDto is null) return TypedResults.NotFound();

        return TypedResults.Ok(dishDto);
    }
}