using PointOfSale.DataAccess.Order.Entities;
using PointOfSale.DataAccess.Order.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;

namespace PointOfSale.DataAccess.Order.Repositories;

public class ProductRepository : RepositoryBase<Product, int>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}