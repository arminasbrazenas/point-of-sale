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
    private readonly IOrderItemRepository _orderItemRepository;

    public OrderService(
        IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        IOrderRepository orderRepository,
        IOrderMappingService orderMappingService,
        IOrderItemRepository orderItemRepository
    )
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _orderMappingService = orderMappingService;
        _orderItemRepository = orderItemRepository;
    }

    public async Task<OrderMinimalDTO> CreateOrder(CreateOrderDTO createOrderDTO)
    {
        var orderItems = await ReserveOrderItemsFromStock(createOrderDTO.OrderItems);
        var order = new Order { Items = orderItems, Status = OrderStatus.Open };

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

    public async Task<OrderDTO> UpdateOrder(int orderId, UpdateOrderDTO updateOrderDTO)
    {
        var order = await _orderRepository.GetWithOrderItems(orderId);
        if (order.Status != OrderStatus.Open)
        {
            throw new ValidationException(new CannotModifyNonOpenOrderErrorMessage());
        }

        if (updateOrderDTO.OrderItems is not null)
        {
            await ReturnOrderItemsToStock(order.Items);
            _orderItemRepository.DeleteMany(order.Items);
            order.Items = await ReserveOrderItemsFromStock(updateOrderDTO.OrderItems);
        }

        await _unitOfWork.SaveChanges();

        return _orderMappingService.MapToOrderDTO(order);
    }

    public async Task CancelOrder(int orderId)
    {
        var order = await _orderRepository.GetWithOrderItems(orderId);
        if (order.Status != OrderStatus.Open)
        {
            throw new ValidationException(new CannotModifyNonOpenOrderErrorMessage());
        }

        await ReturnOrderItemsToStock(order.Items);
        order.Status = OrderStatus.Canceled;

        await _unitOfWork.SaveChanges();
    }

    private async Task<List<OrderItem>> ReserveOrderItemsFromStock(List<CreateOrderItemDTO> createOrderItemDTOs)
    {
        var groupedOrderItems = createOrderItemDTOs
            .GroupBy(
                o => o.ProductId,
                o => o.Quantity,
                (id, quantities) => new { ProductId = id, Quantity = quantities.Sum() }
            )
            .ToList();

        List<OrderItem> orderItems = [];
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

            orderItems.Add(orderItem);
        }

        return orderItems;
    }

    private async Task ReturnOrderItemsToStock(List<OrderItem> orderItems)
    {
        var groupedOrderItems = orderItems
            .GroupBy(
                i => i.ProductId,
                i => i.Quantity,
                (id, quantities) => new { ProductId = id, Quantity = quantities.Sum() }
            )
            .ToList();

        var productIds = groupedOrderItems.Select(i => i.ProductId);
        var products = await _productRepository.GetMany(productIds);

        var joinResult = groupedOrderItems.Join(
            products,
            i => i.ProductId,
            p => p.Id,
            (left, right) => new { Quantity = left.Quantity, Product = right }
        );

        foreach (var joined in joinResult)
        {
            joined.Product.Stock += joined.Quantity;
        }
    }
}
