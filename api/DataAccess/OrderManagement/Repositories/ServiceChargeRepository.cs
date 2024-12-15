using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Repositories;

public class ServiceChargeRepository : RepositoryBase<ServiceCharge, int>, IServiceChargeRepository
{
    public ServiceChargeRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<List<ServiceCharge>> GetPagedWithTaxes(PaginationFilter paginationFilter, int businessId)
    {
        var query = DbSet.Where(s => s.BusinessId == businessId).OrderBy(s => s.CreatedAt).AsQueryable();
        return await GetPaged(query, paginationFilter);
    }

    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        return new ServiceChargeNotFoundErrorMessage(id);
    }
}
