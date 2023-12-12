using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Context;
using Restaurant.DAL.Entities;
using Restaurant.DAL.Repository.Interfaces;

namespace Restaurant.DAL.Repository
{
    public class DishIngredientsRepository : Repository<DishIngredient>, IDishIngredientsRepository
    {
        public DishIngredientsRepository(RestaurantDbContext context) : base(context)
        {
        }

        private RestaurantDbContext RestaurantContext => (RestaurantDbContext)_context;

        public async Task<IEnumerable<DishIngredient>> GetDishIngredientsByDishIdAsync(int dishId)
        {
            return await RestaurantContext.DishIngredients.Where(di => di.DishId == dishId).ToListAsync();
        }
    }
}
