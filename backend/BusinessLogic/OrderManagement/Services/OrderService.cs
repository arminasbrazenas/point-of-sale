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
    private readonly IModifierRepository _modifierRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderMappingService _orderMappingService;

    public OrderService(
        IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        IModifierRepository modifierRepository,
        IOrderRepository orderRepository,
        IOrderMappingService orderMappingService
    )
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _modifierRepository = modifierRepository;
        _orderRepository = orderRepository;
        _orderMappingService = orderMappingService;
    }

    public async Task<OrderDTO> CreateOrder(CreateOrderDTO createOrderDTO)
    {
        var orderItems = await ReserveOrderItems(createOrderDTO.OrderItems);
        var order = new Order { Items = orderItems, Status = OrderStatus.Open };

        _orderRepository.Add(order);
        await _unitOfWork.SaveChanges();

        return _orderMappingService.MapToOrderDTO(order);
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
            await ReturnOrderItems(order.Items);
            order.Items = await ReserveOrderItems(updateOrderDTO.OrderItems);
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

        await ReturnOrderItems(order.Items);
        order.Status = OrderStatus.Canceled;

        await _unitOfWork.SaveChanges();
    }

    private async Task<List<OrderItem>> ReserveOrderItems(List<CreateOrderItemDTO> createOrderItemDTOs)
    {
        var productIds = createOrderItemDTOs.Select(x => x.ProductId);
        var products = await _productRepository.GetManyWithTaxesAndModifiers(productIds);

        List<OrderItem> orderItems = [];
        foreach (var createOrderItemDTO in createOrderItemDTOs)
        {
            var product = products.FirstOrDefault(x => x.Id == createOrderItemDTO.ProductId);
            if (product is null)
            {
                throw new ValidationException(new ProductNotFoundErrorMessage(createOrderItemDTO.ProductId));
            }

            if (product.Stock < createOrderItemDTO.Quantity)
            {
                throw new ValidationException(new ProductOutOfStockErrorMessage(product.Id, product.Stock));
            }

            product.Stock -= createOrderItemDTO.Quantity;

            List<OrderItemModifier> orderItemModifiers = [];
            foreach (var modifierId in createOrderItemDTO.ModifierIds)
            {
                var modifier = product.Modifiers.FirstOrDefault(x => x.Id == modifierId);
                if (modifier is null)
                {
                    throw new ValidationException(new IncompatibleProductModifierErrorMessage(product.Id, modifierId));
                }

                if (modifier.Stock < createOrderItemDTO.Quantity)
                {
                    throw new ValidationException(new ModifierOutOfStockErrorMessage(modifier.Id, modifier.Stock));
                }

                modifier.Stock -= createOrderItemDTO.Quantity;

                var orderItemModifier = new OrderItemModifier
                {
                    ModifierId = modifier.Id,
                    Name = modifier.Name,
                    Price = modifier.Price,
                };
                orderItemModifiers.Add(orderItemModifier);
            }

            var orderItemTaxes = product.Taxes.Select(t => new OrderItemTax { Name = t.Name, Rate = t.Rate }).ToList();
            var orderItem = new OrderItem
            {
                Name = product.Name,
                Quantity = createOrderItemDTO.Quantity,
                Product = product,
                BaseUnitPrice = product.Price,
                Taxes = orderItemTaxes,
                Modifiers = orderItemModifiers,
            };

            orderItems.Add(orderItem);
        }

        return orderItems;
    }

    private async Task ReturnOrderItems(List<OrderItem> orderItems)
    {
        var productIds = orderItems.Where(x => x.ProductId.HasValue).Select(x => x.ProductId!.Value);
        var modifierIds = orderItems
            .SelectMany(x => x.Modifiers)
            .Where(x => x.ModifierId.HasValue)
            .Select(x => x.ModifierId!.Value);

        var products = await _productRepository.GetMany(productIds);
        var modifiers = await _modifierRepository.GetMany(modifierIds);

        foreach (var orderItem in orderItems)
        {
            var product = products.FirstOrDefault(x => x.Id == orderItem.ProductId);
            if (product is not null)
            {
                product.Stock += orderItem.Quantity;
            }

            foreach (var orderItemModifier in orderItem.Modifiers)
            {
                var modifier = modifiers.FirstOrDefault(x => x.Id == orderItemModifier.ModifierId);
                if (modifier is not null)
                {
                    modifier.Stock += orderItem.Quantity;
                }
            }
        }
    }
}
