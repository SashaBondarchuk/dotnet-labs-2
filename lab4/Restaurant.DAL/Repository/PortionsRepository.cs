using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Context;
using Restaurant.DAL.Entities;
using Restaurant.DAL.Repository.Interfaces;

namespace Restaurant.DAL.Repository
{
    public class PortionsRepository : Repository<Portion>, IPortionsRepository
    {
        public PortionsRepository(RestaurantDbContext context) : base(context)
        {
        }

        private RestaurantDbContext RestaurantContext => (RestaurantDbContext)_context;

        public async Task<IEnumerable<Portion>> GetAllPortionsAsync()
        {
            return await GetPortionsQuery()
                .ToListAsync();
        }

        public async Task<IEnumerable<Portion>> GetDishPortionsByIdAsync(int dishId)
        {
            return await GetPortionsQuery()
                .Where(p => p.DishId == dishId)
                .ToListAsync();
        }

        private IQueryable<Portion> GetPortionsQuery()
        {
            return RestaurantContext.Portions
                .Include(p => p.Unit);
        }
    }
}
