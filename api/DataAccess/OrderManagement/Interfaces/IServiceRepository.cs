using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Interfaces;

public interface IServiceRepository : IRepositoryBase<Service, int>
{
    Task<Service?> GetServiceByName(string name);
    Task<Service> GetWithRelatedData(int serviceId);
    Task<bool> ServiceExists(string name, int businessId);
    Task<List<Service>> GetPaged(PaginationFilter paginationFilter, int businessId);
}