using Microsoft.EntityFrameworkCore;
using PointOfSale.BusinessLogic.Order.Interfaces;
using PointOfSale.BusinessLogic.Order.Services;
using PointOfSale.DataAccess.Order.Interfaces;
using PointOfSale.DataAccess.Order.Repositories;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;

namespace PointOfSale.Api.Extensions;

public static class ConfigureServicesExtensions
{
    public static IServiceCollection AddApiLayer(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        return services;
    }

    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        services.AddScoped<ITaxService, TaxService>();
        
        return services;
    }
    
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<PointOfSaleDbContext>(o => o.UseNpgsql(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITaxRepository, TaxRepository>();
        
        return services;
    }
}