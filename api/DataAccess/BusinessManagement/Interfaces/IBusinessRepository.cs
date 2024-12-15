using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.BusinessManagement.Entities;

namespace PointOfSale.DataAccess.BusinessManagement.Interfaces;

public interface IBusinessRepository : IRepositoryBase<Business, int>
{
    Task<List<Business>> GetPagedBusiness(PaginationFilter paginationFilter);
}
