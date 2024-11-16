using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Interfaces;

public interface IServiceChargeRepository : IRepositoryBase<ServiceCharge, int>
{
    Task<List<ServiceCharge>> GetPagedWithTaxes(PaginationFilter paginationFilter);
}
