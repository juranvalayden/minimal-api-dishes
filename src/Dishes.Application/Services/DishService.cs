using Dishes.Application.Abstractions.Errors;
using Dishes.Application.Dtos;
using Dishes.Application.Interfaces;
using Dishes.Application.Mappers;
using Dishes.Infrastructure.Interfaces;

namespace Dishes.Application.Services;

public class DishService(IDishRepository dishRepository) : IDishService
{
    public async Task<Response> GetDishByIdAsync(Guid id, bool shouldIncludeIngredients, CancellationToken cancellationToken = default)
    {
        var entity = await dishRepository.GetDishByIdAsync(id, shouldIncludeIngredients, cancellationToken);

        if (entity is null) return DishErrors.NotFound(id);

        return Response<DishDto>.Success(entity.ToDishDto());
    }

    public async Task<Response> GetDishByNameAsync(string dishName, bool shouldIncludeIngredients, CancellationToken cancellationToken = default)
    {
        var entity = await dishRepository.GetDishByNameAsync(dishName, shouldIncludeIngredients, cancellationToken);

        if (entity is null) return DishErrors.NotFound(dishName);

        return Response<DishDto>.Success(entity.ToDishDto());
    }

    public async Task<Response> GetDishesAsync(string? name, bool shouldIncludeIngredients, CancellationToken cancellationToken = default)
    {
        var entities = await dishRepository.GetDishesAsync(name, shouldIncludeIngredients, cancellationToken);
        return Response<IEnumerable<DishDto>>.Success(entities.ToDishDtos());
    }

    public async Task<Response> GetIngredientsByDishIdAsync(Guid dishId, CancellationToken cancellationToken = default)
    {
        var entities = await dishRepository.GetIngredientsByDishIdAsync(dishId, cancellationToken);
        return Response<IEnumerable<IngredientDto>>.Success(entities.ToIngredientDtos(dishId));
    }

    public async Task<Response> AddAsync(DishForCreationDto dishForCreationDto, CancellationToken cancellationToken = default)
    {
        var createdEntity = dishRepository.Add(dishForCreationDto.ToDish());
        
        var hasSaved = await dishRepository.SaveChangesAsync(cancellationToken) > 0;

        if (!hasSaved) return DishErrors.NotSaved(dishForCreationDto.Name);

        return Response<DishDto>.Success(createdEntity.ToDishDto());
    }

    public async Task<Response> UpdateAsync(Guid dishId, DishForUpdateDto dishForUpdateDto, CancellationToken cancellationToken = default)
    {
        var existingEntity = await dishRepository.GetDishByIdAsync(dishId, false, cancellationToken);

        if (existingEntity is null) return DishErrors.NotFound(dishId);

        var updatedDish = existingEntity.ToDishForUpdate(dishForUpdateDto);
        var updatedEntity = dishRepository.Update(updatedDish);

        var hasSaved = await dishRepository.SaveChangesAsync(cancellationToken) > 0;

        if (!hasSaved) return DishErrors.NotSaved(dishForUpdateDto.Name);

        return Response<DishDto>.Success(updatedEntity.ToDishDto());
    }

    public async Task<Response> DeleteAsync(Guid dishId, CancellationToken cancellationToken = default)
    {
        var entityToBeDeleted = await dishRepository.GetDishByIdAsync(dishId, false, cancellationToken);

        if (entityToBeDeleted is null) return DishErrors.NotFound(dishId);

        var deletedEntity = dishRepository.Delete(entityToBeDeleted);
        var hasSaved = await dishRepository.SaveChangesAsync(cancellationToken) > 0;

        if (!hasSaved) return DishErrors.NotSaved(message: $"Error occurred when deleting dish with {dishId}");

        return Response<DishDto>.Success(deletedEntity.ToDishDto());
    }
}