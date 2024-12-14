using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.ServiceManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.Models.ServiceManagement.Entities;
namespace PointOfSale.DataAccess.ServiceManagement.Repositories;

public class ServiceAvailabilityRepository : RepositoryBase<ServiceAvailability, int>, IServiceAvailabilityRepository
{
    public ServiceAvailabilityRepository(ApplicationDbContext dbContext) 
        : base(dbContext) { }

    public List<int> GetServiceAvailabilityByServiceId(int serviceId)
    {
        return DbSet.Where(s => s.ServiceId == serviceId).Select(s => s.ServiceResourceId).ToList();
    }

    public int GetServiceAvailabilityResourceIdByPriority(int serviceId, List<int> serviceResourceIds)
    {
        var serviceAvailability = DbSet
            .Where(sa => sa.ServiceId == serviceId && serviceResourceIds.Contains(sa.ServiceResourceId))
            .OrderBy(sa => sa.Priority) 
            .FirstOrDefault();
        return serviceAvailability?.ServiceResourceId ?? 0;
    }
    
    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        throw new NotImplementedException();
    }
}