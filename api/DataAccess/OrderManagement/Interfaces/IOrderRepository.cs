using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Interfaces;

public interface IOrderRepository : IRepositoryBase<Order, int>
{
    Task<List<Order>> GetWithFilter(PaginationFilter paginationFilter, int businessId);
    Task<Order> GetWithOrderItems(int orderId);
    Task<int> GetTotalCount(int businessId);
}
