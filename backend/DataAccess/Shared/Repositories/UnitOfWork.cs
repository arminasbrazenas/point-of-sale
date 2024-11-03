using Microsoft.Extensions.DependencyInjection;
using PointOfSale.DataAccess.Order.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.Shared.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly PointOfSaleDbContext _dbContext;
    private readonly IServiceProvider _serviceProvider;

    public UnitOfWork(PointOfSaleDbContext dbContext, IServiceProvider serviceProvider)
    {
        _dbContext = dbContext;
        _serviceProvider = serviceProvider;
    }

    public ITaxRepository Taxes => _serviceProvider.GetRequiredService<ITaxRepository>();
    
    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }
}