using System;
using System.Collections.Generic;
using System.Text;
using Dishes.Domain.Entities;
using Dishes.Infrastructure.DbContexts;
using Dishes.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Dishes.Infrastructure.Repositories;

internal class DishesRepository(ILogger<DishesRepository> logger, DishesDbContext dishesDbContext) : IDishesRepository
{
    public async Task<Dish?> GetDishByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await dishesDbContext.Dishes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error occurred when retrieving dish with {Id}.", id);
            throw;
        }
    }
}