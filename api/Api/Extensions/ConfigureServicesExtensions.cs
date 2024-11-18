using System.Text.Json;
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
    public static IServiceCollection AddSharedServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment
    )
    {
        var connectionString = configuration.GetConnectionString("Database");
        services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

        if (environment.IsDevelopment())
        {
            services.AddCors(options =>
                options.AddDefaultPolicy(policyBuilder =>
                    policyBuilder
                        .SetIsOriginAllowed(origin => new Uri(origin).IsLoopback)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                )
            );
        }

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static IServiceCollection AddOrderManagement(this IServiceCollection services)
    {
        services.AddScoped<ITaxRepository, TaxRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IModifierRepository, ModifierRepository>();
        services.AddScoped<IServiceChargeRepository, ServiceChargeRepository>();

        services.AddScoped<ITaxMappingService, TaxMappingService>();
        services.AddScoped<IProductMappingService, ProductMappingService>();
        services.AddScoped<IOrderMappingService, OrderMappingService>();
        services.AddScoped<IModifierMappingService, ModifierMappingService>();
        services.AddScoped<IServiceChargeMappingService, ServiceChargeMappingService>();

        services.AddScoped<ITaxValidationService, TaxValidationService>();
        services.AddScoped<IProductValidationService, ProductValidationService>();

        services.AddScoped<ITaxService, TaxService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IModifierService, ModifierService>();
        services.AddScoped<IServiceChargeService, ServiceChargeService>();

        return services;
    }
}
