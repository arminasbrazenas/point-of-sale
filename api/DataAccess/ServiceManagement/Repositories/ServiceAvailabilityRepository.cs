using PointOfSale.DataAccess.ServiceManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.Models.ServiceManagement.Entities;
namespace PointOfSale.DataAccess.ServiceManagement.Repositories;

public class ServiceAvailabilityRepository : RepositoryBase<ServiceAvailability, int>, IServiceAvailabilityRepository
{
    public ServiceAvailabilityRepository(ApplicationDbContext dbContext) 
        : base(dbContext) { }

    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        throw new NotImplementedException();
    }
}