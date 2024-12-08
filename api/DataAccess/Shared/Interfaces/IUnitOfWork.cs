namespace PointOfSale.DataAccess.Shared.Interfaces;

public interface IUnitOfWork
{
    Task ExecuteInTransaction(Func<Task> action);
    Task SaveChanges();
}
