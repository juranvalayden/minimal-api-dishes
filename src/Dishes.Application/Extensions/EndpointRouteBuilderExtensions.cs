using Dishes.Application.EndpointHandlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Dishes.Application.Extensions;

public static class EndpointRouteBuilderExtensions
{
    extension(IEndpointRouteBuilder endpointRouteBuilder)
    {
        public void RegisterDishesEndpoints()
        {
            var dishesEndpoints = endpointRouteBuilder
                .MapGroup("/dishes")
                .WithTags("Dishes");

            var dishWithGuidIdEndpoints = dishesEndpoints.MapGroup("/{dishId:guid}");

            dishesEndpoints
                .MapGet("", DishHandler.GetDishesAsync)
                .WithSummary("Gets all the dishes")
                .WithDescription("Returns all the dishes, optionally, can be used to filtered by name. NB! Pagination to be added later...");
            
            dishWithGuidIdEndpoints
                .MapGet("", DishHandler.GetDishByIdAsync)
                .WithName("GetDish")
                .WithSummary("Gets a single dish by id")
                .WithDescription("Returns a single dish identified by the id.");

            dishesEndpoints
                .MapGet("/{dishName}", DishHandler.GetDishByNameAsync)
                .AllowAnonymous()
                .WithSummary("Gets a single dish by its name")
                .WithDescription("Returns a single dish identified by the dish name.");

            dishesEndpoints
                .MapPost("", DishHandler.CreateDishAsync)
                .WithSummary("Creates a single dish")
                .WithDescription("Creates a new dish.")
                .ProducesValidationProblem();

            dishWithGuidIdEndpoints
                .MapPut("", DishHandler.UpdateDishAsync)
                .WithSummary("Updates a single dish")
                .WithDescription("Updates a single dish if the dish can be found.")
                .ProducesValidationProblem();

            dishWithGuidIdEndpoints
                .MapDelete("", DishHandler.DeleteDishAsync)
                .WithSummary("Deletes a single dish")
                .WithDescription("Deletes a single dish if the dish can be found.");
        }

        public void RegisterIngredientsEndpoints()
        {
            var ingredientsEndpoints = endpointRouteBuilder
                .MapGroup("/dishes/{dishId:guid}/ingredients")
                .WithTags("Ingredients");
                
            ingredientsEndpoints
                .MapGet("", IngredientsHandler.GetIngredientsByDishIdAsync)
                .WithSummary("Get ingredients by dish id")
                .WithDescription("Gets all the ingredients for a dish, if the dish can be found.");
        }
    }
}