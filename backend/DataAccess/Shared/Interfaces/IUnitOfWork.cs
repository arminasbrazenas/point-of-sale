using PointOfSale.DataAccess.Order.Interfaces;

namespace PointOfSale.DataAccess.Shared.Interfaces;

public interface IUnitOfWork
{
    ITaxRepository Taxes { get; }
    Task SaveChanges();
}