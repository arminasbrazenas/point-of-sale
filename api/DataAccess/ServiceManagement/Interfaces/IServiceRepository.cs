using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.ServiceManagement.Entities;

namespace PointOfSale.DataAccess.ServiceManagement.Interfaces;

public interface IServiceRepository : IRepositoryBase<Service, int>
{
    public Task<Service?> GetServiceByName(string name);
}