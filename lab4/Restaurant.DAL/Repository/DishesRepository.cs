using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Context;
using Restaurant.DAL.Entities;
using Restaurant.DAL.Repository.Interfaces;

namespace Restaurant.DAL.Repository
{
    public class DishesRepository : Repository<Dish>, IDishesRepository
    {
        public DishesRepository(RestaurantDbContext context) : base(context)
        {
        }

        private RestaurantDbContext RestaurantContext => (RestaurantDbContext)_context;

        public async Task<IEnumerable<Dish>> GetDishesWithAllInfoAsync()
        {
            return await IncludeRelatedEntities(RestaurantContext.Dishes)
                .ToListAsync();
        }

        public async Task<Dish?> GetDishWithAllInfoByIdAsync(int dishId)
        {
            return await IncludeRelatedEntities(RestaurantContext.Dishes.Where(d => d.Id == dishId))
                .FirstOrDefaultAsync();
        }

        private static IQueryable<Dish> IncludeRelatedEntities(IQueryable<Dish> query)
        {
            return query
                .Include(d => d.Ingredients)
                .Include(d => d.Portions)
                    .ThenInclude(p => p.Unit);
        }
    }
}
