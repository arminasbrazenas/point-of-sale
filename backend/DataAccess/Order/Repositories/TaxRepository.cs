using PointOfSale.DataAccess.Order.Entities;
using PointOfSale.DataAccess.Order.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;

namespace PointOfSale.DataAccess.Order.Repositories;

public class TaxRepository : RepositoryBase<Tax, int>, ITaxRepository
{
    public TaxRepository(PointOfSaleDbContext dbContext)
        : base(dbContext) { }
}
