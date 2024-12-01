using PointOfSale.DataAccess.BusinessManagement.ErrorMessages;
using PointOfSale.DataAccess.BusinessManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.Models.BusinessManagement.Entities;

namespace PointOfSale.DataAccess.BusinessManagement.Repositories;

public class BusinessRepository : RepositoryBase<Business, int>, IBusinessRepository
{
    public BusinessRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        return new BusinessNotFoundErrorMessage(id);
    }
}
