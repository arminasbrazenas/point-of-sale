namespace PointOfSale.DataAccess.Shared.Interfaces;

public interface IUnitOfWork
{
    Task SaveChanges();
}
