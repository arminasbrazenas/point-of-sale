using PointOfSale.DataAccess.Order.Entities;
using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.Order.Interfaces;

public interface IProductRepository : IRepositoryBase<Product, int>
{
}