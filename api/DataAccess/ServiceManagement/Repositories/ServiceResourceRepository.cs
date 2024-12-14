using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.ServiceManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.Models.ServiceManagement.Entities;

namespace PointOfSale.DataAccess.ServiceManagement.Repositories;

public class ServiceResourceRepository : RepositoryBase<ServiceResource, int>, IServiceResourceRepository
{
    public ServiceResourceRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<ServiceResource?> GetServiceResourceByName(string name)
    {
        return await DbSet.FirstOrDefaultAsync(s => s.Name == name);
    }
    
    
    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        throw new NotImplementedException();
    }
}