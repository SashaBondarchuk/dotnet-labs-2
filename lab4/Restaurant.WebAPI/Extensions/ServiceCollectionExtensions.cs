using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Context;
using System.Reflection;

namespace Restaurant.WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRestaurantDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionsString = configuration.GetConnectionString("RestaurantDbConnection");
            services.AddDbContext<RestaurantDbContext>(options =>
                options.UseSqlServer(
                    connectionsString,
                    opt => opt.MigrationsAssembly(typeof(RestaurantDbContext).Assembly.GetName().Name)));
        }

        public static void RegisterCustomServices(this IServiceCollection services)
        {

        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            //services.AddAutoMapper(Assembly.GetAssembly(typeof(Profile)));
        }
    }
}
