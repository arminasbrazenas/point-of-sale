using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Interfaces;

public interface IDiscountRepository : IRepositoryBase<Discount, int>
{
    Task<Discount> GetWithProducts(int discountId);
    Task<List<Discount>> GetPagedWithProducts(PaginationFilter paginationFilter, int businessId);
    Task<List<Discount>> GetOrderDiscounts();
    Task<int> GetTotalCount(int businessId);
}
