using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Interfaces;

public interface ITaxRepository : IRepositoryBase<Tax, int>
{
    Task<Tax?> GetByNameOptional(string name);
    Task<List<Tax>> GetPaged(PaginationFilter paginationFilter);
}
