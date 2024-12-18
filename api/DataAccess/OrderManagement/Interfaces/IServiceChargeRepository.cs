using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Interfaces;

public interface IServiceChargeRepository : IRepositoryBase<ServiceCharge, int>
{
    Task<ServiceCharge?> GetByNameOptional(string name, int businessId);
    Task<List<ServiceCharge>> GetPagedWithTaxes(PaginationFilter paginationFilter, int businessId);
    Task<int> GetTotalCount(int businessId);
}
