using Restaurant.DAL.Entities;

namespace Restaurant.DAL.Repository.Interfaces
{
    public interface IDishIngredientsRepository : IRepository<DishIngredient>
    {
        Task<IEnumerable<DishIngredient>> GetDishIngredientsByDishIdAsync(int dishId);
    }
}
