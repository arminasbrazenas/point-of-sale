using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Interfaces;

public interface ITaxRepository : IRepositoryBase<Tax, int>
{
    Task<Tax?> GetByNameOptional(string name, int businessId);
    Task<List<Tax>> GetPaged(int businessId, PaginationFilter paginationFilter);
    Task<int> GetTotalCount(int businessId);
}
