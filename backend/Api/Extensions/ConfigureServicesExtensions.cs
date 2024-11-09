using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.OrderManagement.Services;
using PointOfSale.DataAccess;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.OrderManagement.Repositories;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;

namespace PointOfSale.Api.Extensions;

public static class ConfigureServicesExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static IServiceCollection AddOrderManagement(this IServiceCollection services)
    {
        services.AddScoped<ITaxRepository, TaxRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddScoped<ITaxService, TaxService>();
        services.AddScoped<ITaxMappingService, TaxMappingService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductValidationService, ProductValidationService>();
        services.AddScoped<IProductMappingService, ProductMappingService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderMappingService, OrderMappingService>();

        return services;
    }
}
