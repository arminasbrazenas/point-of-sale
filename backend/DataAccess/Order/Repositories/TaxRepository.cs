using PointOfSale.DataAccess.Order.Entities;
using PointOfSale.DataAccess.Order.ErrorMessages;
using PointOfSale.DataAccess.Order.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;

namespace PointOfSale.DataAccess.Order.Repositories;

public class TaxRepository : RepositoryBase<Tax, int>, ITaxRepository
{
    public TaxRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    protected override IPointOfSaleErrorMessage CreateEntityNotFoundErrorMessage(int id)
    {
        return new TaxNotFoundErrorMessage(id);
    }
}
