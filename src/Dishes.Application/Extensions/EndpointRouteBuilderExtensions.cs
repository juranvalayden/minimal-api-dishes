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
            var dishWithGuidIdEndpoints = dishesEndpoints.MapGroup("/{dishId:guid}");
            
            dishesEndpoints.MapGet("", DishHandler.GetDishesAsync);
            
            dishWithGuidIdEndpoints
                .MapGet("", DishHandler.GetDishByIdAsync)
                .WithName("GetDish");

            dishesEndpoints
                .MapGet("/{dishName}", DishHandler.GetDishByNameAsync)
                .AllowAnonymous();

            dishesEndpoints.MapPost("", DishHandler.CreateDishAsync);
            dishWithGuidIdEndpoints.MapPut("", DishHandler.UpdateDishAsync);
            dishWithGuidIdEndpoints.MapDelete("", DishHandler.DeleteDishAsync);
        }

        public void RegisterIngredientsEndpoints()
        {
            var ingredientsEndpoints = endpointRouteBuilder
                .MapGroup("/dishes/{dishId:guid}/ingredients")
                .RequireAuthorization();
            ingredientsEndpoints.MapGet("", IngredientsHandler.GetIngredientsByDishIdAsync);
        }
    }
}