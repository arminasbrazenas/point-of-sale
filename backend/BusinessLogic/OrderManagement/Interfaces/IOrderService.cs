using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IOrderService
{
    Task<OrderMinimalDTO> CreateOrder(CreateOrderDTO createOrderDTO);
    Task<PagedResponseDTO<OrderMinimalDTO>> GetOrders(PaginationFilterDTO paginationFilterDTO);
    Task<OrderDTO> GetOrder(int orderId);
    Task<OrderDTO> UpdateOrder(int orderId, UpdateOrderDTO updateOrderDTO);
    Task CancelOrder(int orderId);
}
