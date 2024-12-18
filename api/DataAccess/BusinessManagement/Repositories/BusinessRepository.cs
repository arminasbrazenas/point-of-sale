using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.BusinessManagement.ErrorMessages;
using PointOfSale.DataAccess.BusinessManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Exceptions;
using PointOfSale.DataAccess.Shared.Filters;
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

    public async Task<List<Business>> GetPagedBusiness(PaginationFilter paginationFilter)
    {
        return await GetPaged(DbSet.Where(b => b.IsActive).OrderBy(b => b.Id), paginationFilter);
    }

    public override async Task<int> GetTotalCount()
    {
        return await DbSet.Where(b => b.IsActive).CountAsync();
    }

    public async Task<Business> GetActive(int id)
    {
        var entity = await DbSet.FindAsync(id);
        if (entity is not null && entity.IsActive)
        {
            return entity;
        }

        var errorMessage = GetEntityNotFoundErrorMessage(id);
        throw new EntityNotFoundException(errorMessage);
    }

    public async Task<Business> GetWithEmployees(int id)
    {
        var entity = await DbSet.Include(b => b.Employees).FirstOrDefaultAsync(b => b.Id == id);
        if (entity is not null && entity.IsActive)
        {
            return entity;
        }

        var errorMessage = GetEntityNotFoundErrorMessage(id);
        throw new EntityNotFoundException(errorMessage);
    }
}
