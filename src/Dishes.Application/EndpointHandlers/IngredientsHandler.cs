using Dishes.Application.Abstractions.Errors;
using Dishes.Application.Dtos;
using Dishes.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Dishes.Application.EndpointHandlers;

public static class IngredientsHandler
{
    public static async Task<Results<BadRequest, Ok<IEnumerable<IngredientDto>>>> GetIngredientsByDishIdAsync(Guid dishId, IDishService dishService, CancellationToken cancellationToken)
    {
        var response = await dishService.GetIngredientsByDishIdAsync(dishId, cancellationToken);

        if (response is Response<IEnumerable<IngredientDto>> success)
        {
            return TypedResults.Ok(success.Data);
        }

        return TypedResults.BadRequest();
    }
}
