using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.ServiceManagement.Entities;

namespace PointOfSale.DataAccess.ServiceManagement.Interfaces;

public interface IServiceAvailabilityRepository : IRepositoryBase<ServiceAvailability, int>
{
    
}