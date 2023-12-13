using Restaurant.Common.Filters;
using Restaurant.WebAPI.Extensions;

namespace Restaurant.WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers(options => options.Filters.Add<CustomExceptionFilterAttribute>());

        builder.Services.AddRestaurantDbContext(builder.Configuration);
        builder.Services.RegisterRepositories();
        builder.Services.RegisterCustomServices();
        builder.Services.AddAutoMapper();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}


