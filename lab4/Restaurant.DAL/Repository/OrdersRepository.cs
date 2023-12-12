using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Context;
using Restaurant.DAL.Entities;
using Restaurant.DAL.Repository.Interfaces;

namespace Restaurant.DAL.Repository
{
    public class OrdersRepository : Repository<Order>, IOrdersRepository
    {
        public OrdersRepository(RestaurantDbContext context) : base(context)
        {
        }

        private RestaurantDbContext RestaurantContext => (RestaurantDbContext)_context;

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await GetOrdersQuery()
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await GetOrdersQuery()
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        private IQueryable<Order> GetOrdersQuery()
        {
            return RestaurantContext.Orders
                .Include(p => p.OrderItems)
                    .ThenInclude(i => i.Portion)
                        .ThenInclude(p => p.Unit);
        }
    }
}
