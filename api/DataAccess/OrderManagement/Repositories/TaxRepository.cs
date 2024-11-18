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

    public async Task<Tax?> GetByNameOptional(string name)
    {
        return await DbSet.FirstOrDefaultAsync(t => t.Name == name);
    }

    public async Task<List<Tax>> GetPaged(PaginationFilter paginationFilter)
    {
        var query = DbSet.AsQueryable();
        return await GetPaged(query, paginationFilter);
    }

    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        return new TaxNotFoundErrorMessage(id);
    }
}
