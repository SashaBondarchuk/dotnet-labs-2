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

        //public RestaurantDbContext RestaurantDbContext { get { return (RestaurantDbContext)_context; } }
    }
}
