using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Repositories;

public class TaxRepository : RepositoryBase<Tax, int>, ITaxRepository
{
    public TaxRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<Tax?> GetByNameOptional(string name, int businessId)
    {
        return await DbSet.FirstOrDefaultAsync(t => t.Name == name && t.BusinessId == businessId);
    }

    public async Task<List<Tax>> GetPaged(int businessId, PaginationFilter paginationFilter)
    {
        var query = DbSet.Where(t => t.BusinessId == businessId).OrderBy(t => t.CreatedAt).AsQueryable();
        return await GetPaged(query, paginationFilter);
    }

    public async Task<int> GetTotalCount(int businessId)
    {
        return await DbSet.Where(t => t.BusinessId == businessId).CountAsync();
    }

    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        return new TaxNotFoundErrorMessage(id);
    }
}
