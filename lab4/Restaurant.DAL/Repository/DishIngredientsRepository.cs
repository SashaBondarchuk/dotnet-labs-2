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
    }
}
