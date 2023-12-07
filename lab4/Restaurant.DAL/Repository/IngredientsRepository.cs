using Restaurant.DAL.Context;
using Restaurant.DAL.Entities;
using Restaurant.DAL.Repository.Interfaces;

namespace Restaurant.DAL.Repository
{
    public class IngredientsRepository : Repository<Ingredient>, IIngredientsRepository
    {
        public IngredientsRepository(RestaurantDbContext context) : base(context)
        {
        }
    }
}
