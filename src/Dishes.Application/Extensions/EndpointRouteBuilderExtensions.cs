using Dishes.Application.EndpointHandlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Dishes.Application.Extensions;

public static class EndpointRouteBuilderExtensions
{
    extension(IEndpointRouteBuilder endpointRouteBuilder)
    {
        public void RegisterDishesEndpoints()
        {
            var dishesEndpoints = endpointRouteBuilder.MapGroup("/dishes");
            var dishWithDishId = dishesEndpoints.MapGroup("/{dishId:guid}");
            var dishWithDishName = dishesEndpoints.MapGroup("/{dishName}");

            dishesEndpoints.MapGet("", DishesHandler.GetDishesAsync);
            dishWithDishId.MapGet("", DishesHandler.GetDishByIdAsync);
            dishWithDishName.MapGet("", DishesHandler.GetDishByNameAsync);
        }

        public void RegisterIngredientsEndpoints()
        {
            var ingredientsEndpoints = endpointRouteBuilder.MapGroup("/dishes/{dishId:guid}/ingredients");
            ingredientsEndpoints.MapGet("", IngredientsHandler.GetIngredientsByDishIdAsync);
        }
    }
}