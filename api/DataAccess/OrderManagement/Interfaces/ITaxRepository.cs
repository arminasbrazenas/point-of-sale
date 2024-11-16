using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Interfaces;

public interface ITaxRepository : IRepositoryBase<Tax, int>
{
    Task<List<Tax>> GetPaged(PaginationFilter paginationFilter);
}
