using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public Task<OrderDTO> CreateOrder(CreateOrderDTO createOrderDTO)
    {
        throw new NotImplementedException();
    }
}
