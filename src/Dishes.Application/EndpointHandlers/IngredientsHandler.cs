using Dishes.Application.Dtos;
using Dishes.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Dishes.Application.EndpointHandlers;

public static class IngredientsHandler
{
    public static async Task<Ok<IEnumerable<IngredientDto>>> GetIngredientsByDishIdAsync(Guid dishId, IDishService dishService, CancellationToken cancellationToken)
    {
        var ingredientDtos = await dishService.GetIngredientsByDishIdAsync(dishId, cancellationToken);

        return TypedResults.Ok(ingredientDtos);
    }
}
