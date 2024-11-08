using PointOfSale.BusinessLogic.OrderManagement.DTOs;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IOrderService
{
    Task<OrderDTO> CreateOrder(CreateOrderDTO createOrderDTO);
}
