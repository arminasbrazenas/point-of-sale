using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.BusinessLogic.Shared.Factories;
using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.OrderManagement.Enums;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderMappingService _orderMappingService;

    public OrderService(
        IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        IOrderRepository orderRepository,
        IOrderMappingService orderMappingService
    )
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _orderMappingService = orderMappingService;
    }

    public async Task<OrderMinimalDTO> CreateOrder(CreateOrderDTO createOrderDTO)
    {
        var order = new Order { Items = [], Status = OrderStatus.Open };

        var groupedOrderItems = createOrderDTO.OrderItems.GroupBy(
            o => o.ProductId,
            o => o.Quantity,
            (id, quantities) => new { ProductId = id, Quantity = quantities.Sum() }
        );

        foreach (var groupedOrderItem in groupedOrderItems)
        {
            var product = await _productRepository.GetWithTaxes(groupedOrderItem.ProductId);
            if (product.Stock < groupedOrderItem.Quantity)
            {
                throw new ValidationException(new ProductOutOfStockErrorMessage(product.Name, product.Stock));
            }

            product.Stock -= groupedOrderItem.Quantity;

            var taxes = product.Taxes.Select(t => new OrderItemTax { TaxName = t.Name, TaxRate = t.Rate }).ToList();

            var orderItem = new OrderItem
            {
                Name = product.Name,
                Quantity = groupedOrderItem.Quantity,
                Product = product,
                BaseUnitPrice = product.Price,
                Taxes = taxes,
            };

            order.Items.Add(orderItem);
        }

        _orderRepository.Add(order);
        await _unitOfWork.SaveChanges();

        return _orderMappingService.MapToOrderMinimalDTO(order);
    }

    public async Task<PagedResponseDTO<OrderMinimalDTO>> GetOrders(PaginationFilterDTO paginationFilterDTO)
    {
        var paginationFilter = PaginationFilterFactory.Create(paginationFilterDTO);
        var orders = await _orderRepository.GetMinimalWithFilter(paginationFilter);
        return _orderMappingService.MapToPagedOrderMinimalDTO(orders, paginationFilter);
    }

    public async Task<OrderDTO> GetOrder(int orderId)
    {
        var order = await _orderRepository.GetWithOrderItems(orderId);
        return _orderMappingService.MapToOrderDTO(order);
    }
}
