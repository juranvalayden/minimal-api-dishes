using System;
using System.Collections.Generic;
using System.Text;
using Dishes.Application.Dtos;
using Dishes.Application.Interfaces;
using Dishes.Infrastructure.Interfaces;

namespace Dishes.Application.Services;

public class DishesService(IDishesRepository dishRepository, IMapper mapper) : IDishService
{
    private readonly IDishesRepository _dishRepository = dishRepository ?? throw new ArgumentNullException(nameof(dishRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<DishesDto?> GetDishByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var dish = await _dishRepository.GetDishByIdAsync(id, cancellationToken);

        return dish is null 
            ? null 
            : _mapper.Map(dish);
    }
}