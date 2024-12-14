using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.ServiceManagement.Entities;

namespace PointOfSale.DataAccess.ServiceManagement.Interfaces;

public interface IServiceResourceRepository : IRepositoryBase<ServiceResource, int>
{
    public Task<ServiceResource?> GetServiceResourceByName(string name);
}