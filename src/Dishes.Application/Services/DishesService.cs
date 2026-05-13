using Dishes.Application.Abstractions.Errors;
using Dishes.Application.Dtos;
using Dishes.Application.Interfaces;
using Dishes.Infrastructure.Interfaces;

namespace Dishes.Application.Services;

public class DishesService(IDishesRepository dishRepository, IMapper mapper) : IDishService
{
    public async Task<Response> GetDishByIdAsync(Guid id, bool shouldIncludeIngredients, CancellationToken cancellationToken = default)
    {
        var entity = await dishRepository.GetDishByIdAsync(id, shouldIncludeIngredients, cancellationToken);

        if (entity is null) return DishErrors.NotFound(id);

        var dishDto = mapper.Map(entity);
        return Response<DishDto>.Success(dishDto);
    }

    public async Task<Response> GetDishByNameAsync(string dishName, bool shouldIncludeIngredients, CancellationToken cancellationToken = default)
    {
        var entity = await dishRepository.GetDishByNameAsync(dishName, shouldIncludeIngredients, cancellationToken);

        if (entity is null) return DishErrors.NotFound(dishName);

        var dishDto = mapper.Map(entity);
        return Response<DishDto>.Success(dishDto);
    }

    public async Task<Response> GetDishesAsync(string? name, bool shouldIncludeIngredients, CancellationToken cancellationToken = default)
    {
        var entities = await dishRepository.GetDishesAsync(name, shouldIncludeIngredients, cancellationToken);

        var dishes = entities.ToList();

        return Response<IEnumerable<DishDto>>.Success(mapper.Map(dishes));
    }

    public async Task<Response> GetIngredientsByDishIdAsync(Guid dishId, CancellationToken cancellationToken = default)
    {
        var entities = await dishRepository.GetIngredientsByDishIdAsync(dishId, cancellationToken);

        var ingredients = entities.ToList();

        if (ingredients.Count == 0)
        {
            return Response<IEnumerable<IngredientDto>>.Success([]);
        }

        var ingredientDtos = mapper.Map(dishId, ingredients);
        return Response<IEnumerable<IngredientDto>>.Success(ingredientDtos);
    }

    public async Task<Response> AddAsync(DishForCreationDto dishForCreationDto, CancellationToken cancellationToken = default)
    {
        var createdEntity = mapper.MapDtoToEntity(dishForCreationDto);
        var entity = dishRepository.Add(createdEntity);
        var hasSaved = await dishRepository.SaveChangesAsync(cancellationToken) > 0;

        if (!hasSaved) return DishErrors.NotSaved(dishForCreationDto.Name);

        var createdDto = mapper.Map(entity);
        return Response<DishDto>.Success(createdDto);
    }

    public async Task<Response> UpdateAsync(Guid dishId, DishForUpdateDto dishForUpdateDto, CancellationToken cancellationToken = default)
    {
        var existingEntity = await dishRepository.GetDishByIdAsync(dishId, false, cancellationToken);

        if (existingEntity is null) return DishErrors.NotFound(dishId);

        var updatedEntity = mapper.MapDtoToEntity(existingEntity, dishForUpdateDto);
        var entity = dishRepository.Update(updatedEntity);
        var hasSaved = await dishRepository.SaveChangesAsync(cancellationToken) > 0;

        if (!hasSaved) return DishErrors.NotSaved(dishForUpdateDto.Name);

        var updatedDto = mapper.Map(entity);
        return Response<DishDto>.Success(updatedDto);
    }

    public async Task<Response> DeleteAsync(Guid dishId, CancellationToken cancellationToken = default)
    {
        var existingEntity = await dishRepository.GetDishByIdAsync(dishId, false, cancellationToken);

        if (existingEntity is null) return DishErrors.NotFound(dishId);

        var deletedEntity = dishRepository.Delete(existingEntity);
        var hasSaved = await dishRepository.SaveChangesAsync(cancellationToken) > 0;

        if (!hasSaved) return DishErrors.NotSaved(message: $"Error occurred when deleting dish with {dishId}");

        var deletedDto = mapper.Map(deletedEntity);
        return Response<DishDto>.Success(deletedDto);
    }
}