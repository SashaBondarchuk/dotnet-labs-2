using Restaurant.DAL.Entities;

namespace Restaurant.DAL.Repository.Interfaces
{
    public interface IDishesRepository : IRepository<Dish>
    {
        Task<IEnumerable<Dish>> GetDishesWithAllInfoAsync();

        Task<Dish?> GetDishWithAllInfoByIdAsync(int dishId);
    }
}
