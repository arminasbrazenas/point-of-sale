using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Repositories;

public class ServiceRepository : RepositoryBase<Service, int>, IServiceRepository
{
    public ServiceRepository(ApplicationDbContext dbContext) 
        : base(dbContext) { }

    public async Task<Service?> GetServiceByName(string name)
    {
        return await DbSet.FirstOrDefaultAsync(s => s.Name == name);
    }
    
    public async Task<List<Service>> GetPaged(PaginationFilter paginationFilter)
    {
        var query = DbSet.OrderBy(s => s.CreatedAt).AsQueryable();
        return await GetPaged(query, paginationFilter);
    }
    
    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        return new ServiceNotFoundErrorMessage(id);
    }
}