using Dishes.Application.Dtos;
using Dishes.Domain.Entities;

namespace Dishes.Application.Interfaces;

public interface IMapper
{
    DishesDto Map(Dish dish);
}