using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.Shared.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }
}
