using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Exceptions;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Repositories;

public class ServiceRepository : RepositoryBase<Service, int>, IServiceRepository
{
    public ServiceRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<Service> GetWithRelatedData(int serviceId)
    {
        var service = await DbSet.Include(s => s.ProvidedByEmployees).FirstOrDefaultAsync(s => s.Id == serviceId);

        if (service is null)
        {
            throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(serviceId));
        }

        return service;
    }

    public async Task<bool> ServiceExists(string name, int businessId)
    {
        return await DbSet.AnyAsync(s => s.BusinessId == businessId && s.Name == name);
    }

    public async Task<List<Service>> GetPaged(PaginationFilter paginationFilter, int businessId)
    {
        var query = DbSet
            .Where(s => s.BusinessId == businessId)
            .Include(s => s.ProvidedByEmployees)
            .OrderBy(s => s.CreatedAt)
            .AsQueryable();
        return await GetPaged(query, paginationFilter);
    }

    public override async Task<int> GetTotalCount(int? businessId = null)
    {
        if (businessId.HasValue)
        {
            return await DbSet.Where(s => s.BusinessId == businessId).CountAsync();
        }

        return await base.GetTotalCount(businessId);
    }

    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        return new ServiceNotFoundErrorMessage(id);
    }
}
