using Microsoft.EntityFrameworkCore;
using Restaurant.BLL.Interfaces;
using Restaurant.BLL.Mapping_Profiles;
using Restaurant.BLL.Services;
using Restaurant.DAL.Context;
using Restaurant.DAL.Repository;
using Restaurant.DAL.Repository.Interfaces;
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
            services.AddScoped<IIngredientsService, IngredientsService>();
            services.AddScoped<IDishesService, DishesService>();
            services.AddScoped<IPortionsService, PortionsService>();
            services.AddScoped<IOrdersService, OrdersService>();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDishesRepository, DishesRepository>();
            services.AddScoped<IDishIngredientsRepository, DishIngredientsRepository>();
            services.AddScoped<IIngredientsRepository, IngredientsRepository>();
            services.AddScoped<IOrderItemsRepository, OrderItemsRepository>();
            services.AddScoped<IPortionsRepository, PortionsRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetAssembly(typeof(IngredientProfile)));
        }
    }
}
