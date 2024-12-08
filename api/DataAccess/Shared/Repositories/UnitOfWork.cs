using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.Shared.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ExecuteInTransaction(Func<Task> action)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            await action();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }
}
