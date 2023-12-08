using Restaurant.DAL.Context;
using Restaurant.DAL.Entities;
using Restaurant.DAL.Repository.Interfaces;

namespace Restaurant.DAL.Repository
{
    public class OrderItemsRepository : Repository<OrderItem>, IOrderItemsRepository
    {
        public OrderItemsRepository(RestaurantDbContext context) : base(context)
        {
        }
    }
}
