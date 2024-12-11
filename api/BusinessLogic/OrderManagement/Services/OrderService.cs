using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Extensions;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.OrderManagement.Utilities;
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
    private readonly IServiceChargeRepository _serviceChargeRepository;

    public OrderService(
        IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        IModifierRepository modifierRepository,
        IOrderRepository orderRepository,
        IOrderMappingService orderMappingService,
        IServiceChargeRepository serviceChargeRepository
    )
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _modifierRepository = modifierRepository;
        _orderRepository = orderRepository;
        _orderMappingService = orderMappingService;
        _serviceChargeRepository = serviceChargeRepository;
    }

    public async Task<OrderDTO> CreateOrder(CreateOrderDTO createOrderDTO)
    {
        var orderItems = await ReserveOrderItems(createOrderDTO.OrderItems);
        var order = new Order
        {
            BusinessId = createOrderDTO.BusinessId,
            Items = orderItems,
            Status = OrderStatus.Open,
            ServiceCharges = [],
        };

        order.ServiceCharges = await CreateOrderServiceCharges(order, createOrderDTO.ServiceChargeIds);

        _orderRepository.Add(order);
        await _unitOfWork.SaveChanges();

        return _orderMappingService.MapToOrderDTO(order);
    }

    public async Task<PagedResponseDTO<OrderMinimalDTO>> GetOrders(PaginationFilterDTO paginationFilterDTO)
    {
        var paginationFilter = PaginationFilterFactory.Create(paginationFilterDTO);
        var orders = await _orderRepository.GetWithFilter(paginationFilter);
        var totalCount = await _orderRepository.GetTotalCount();
        return _orderMappingService.MapToPagedOrderMinimalDTO(orders, paginationFilter, totalCount);
    }

    public async Task<OrderDTO> GetOrder(int orderId)
    {
        var order = await _orderRepository.GetWithOrderItems(orderId);
        return _orderMappingService.MapToOrderDTO(order);
    }

    public async Task<OrderMinimalDTO> GetOrderMinimal(int orderId)
    {
        var order = await _orderRepository.Get(orderId);
        return _orderMappingService.MapToOrderMinimalDTO(order);
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

        if (updateOrderDTO.ServiceChargeIds is not null)
        {
            var serviceCharges = await CreateOrderServiceCharges(order, updateOrderDTO.ServiceChargeIds);
            order.ServiceCharges = serviceCharges;
        }

        await _unitOfWork.SaveChanges();

        return _orderMappingService.MapToOrderDTO(order);
    }

    public async Task CancelOrder(int orderId)
    {
        var order = await _orderRepository.GetWithOrderItems(orderId);
        if (order.Status != OrderStatus.Open)
        {
            throw new ValidationException(new CannotCancelNonOpenOrderErrorMessage());
        }

        await ReturnOrderItems(order.Items);
        order.Status = OrderStatus.Canceled;

        await _unitOfWork.SaveChanges();
    }

    public async Task<OrderReceiptDTO> GetOrderReceipt(int orderId)
    {
        var order = await _orderRepository.GetWithOrderItems(orderId);
        return _orderMappingService.MapToOrderReceiptDTO(order);
    }

    public async Task CompleteOrder(int orderId)
    {
        var order = await _orderRepository.Get(orderId);
        if (order.Status != OrderStatus.Open)
        {
            throw new ValidationException(new CannotCompleteNonOpenOrderErrorMessage());
        }

        order.Status = OrderStatus.Completed;
        await _unitOfWork.SaveChanges();
    }

    public async Task CloseOrder(int orderId)
    {
        var order = await _orderRepository.Get(orderId);
        if (order.Status != OrderStatus.Completed)
        {
            throw new ValidationException(new CannotCloseNonCompletedOrderErrorMessage());
        }

        order.Status = OrderStatus.Closed;
        await _unitOfWork.SaveChanges();
    }

    private async Task<List<OrderItem>> ReserveOrderItems(List<CreateOrUpdateOrderItemDTO> createOrderItemDTOs)
    {
        var productIds = createOrderItemDTOs.Select(x => x.ProductId);
        var products = await _productRepository.GetManyWithRelatedData(productIds);

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
                throw new ValidationException(new ProductOutOfStockErrorMessage(product.Name));
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
                    throw new ValidationException(new ModifierOutOfStockErrorMessage(modifier.Name));
                }

                modifier.Stock -= createOrderItemDTO.Quantity;

                var orderItemModifier = new OrderItemModifier
                {
                    ModifierId = modifier.Id,
                    Name = modifier.Name,
                    GrossPrice = modifier.Price.ToRoundedPrice(),
                    TaxTotal = 0,
                };

                orderItemModifiers.Add(orderItemModifier);
            }

            // Apply predefined discounts only to the base product
            var baseUnitGrossPrice = product.Price.ToRoundedPrice();
            var orderItemDiscounts = product
                .Discounts.OrderBy(d => d.PricingStrategy)
                .Select(d =>
                {
                    var unitDiscountAmount = d.GetAmountToApply(baseUnitGrossPrice);
                    baseUnitGrossPrice -= unitDiscountAmount;

                    return new OrderItemDiscount
                    {
                        Amount = d.Amount,
                        PricingStrategy = d.PricingStrategy,
                        AppliedUnitAmount = unitDiscountAmount,
                    };
                })
                .ToList();

            // Apply taxes
            var baseUnitNetPrice = baseUnitGrossPrice;
            var orderItemTaxes = product
                .Taxes.Select(t =>
                {
                    var taxTotal = t.GetAmountToApply(baseUnitNetPrice);
                    baseUnitNetPrice += taxTotal;

                    foreach (var orderItemModifier in orderItemModifiers)
                    {
                        var modifierTax = t.GetAmountToApply(orderItemModifier.GrossPrice + orderItemModifier.TaxTotal);
                        orderItemModifier.TaxTotal += modifierTax;
                        taxTotal += modifierTax;
                    }

                    return new OrderItemTax
                    {
                        Name = t.Name,
                        Rate = t.Rate,
                        AppliedUnitAmount = taxTotal,
                    };
                })
                .ToList();

            var orderItem = new OrderItem
            {
                Name = product.Name,
                Quantity = createOrderItemDTO.Quantity,
                Product = product,
                BaseUnitGrossPrice = product.Price.ToRoundedPrice(),
                Taxes = orderItemTaxes,
                Modifiers = orderItemModifiers,
                Discounts = orderItemDiscounts,
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

    private async Task<List<OrderServiceCharge>> CreateOrderServiceCharges(Order order, List<int> serviceChargeIds)
    {
        var serviceCharges = await _serviceChargeRepository.GetMany(serviceChargeIds);
        var orderPrice = order.GetOrderItemsPrice();
        return serviceCharges
            .OrderBy(c => c.PricingStrategy)
            .Select(c =>
            {
                var appliedAmount = c.GetAmountToApply(orderPrice);
                orderPrice += appliedAmount;

                return new OrderServiceCharge
                {
                    Name = c.Name,
                    PricingStrategy = c.PricingStrategy,
                    Amount = c.Amount,
                    AppliedAmount = appliedAmount,
                };
            })
            .ToList();
    }
}
