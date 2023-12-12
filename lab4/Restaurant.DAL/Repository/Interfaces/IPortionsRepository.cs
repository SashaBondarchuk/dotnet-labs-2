using Restaurant.DAL.Entities;

namespace Restaurant.DAL.Repository.Interfaces
{
    public interface IPortionsRepository : IRepository<Portion>
    {
        Task<IEnumerable<Portion>> GetAllPortionsAsync();

        Task<IEnumerable<Portion>> GetDishPortionsByIdAsync(int dishId);
    }
}
