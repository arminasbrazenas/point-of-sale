using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Extensions;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.BusinessLogic.Shared.Factories;
using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.OrderManagement.Enums;
using PointOfSale.Models.OrderManagement.Interfaces;
using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IModifierRepository _modifierRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderMappingService _orderMappingService;
    private readonly IServiceChargeRepository _serviceChargeRepository;
    private readonly IOrderManagementAuthorizationService _orderAuthorizationService;
    private readonly IDiscountRepository _discountRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IReservationService _reservationService;

    public OrderService(
        IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        IModifierRepository modifierRepository,
        IOrderRepository orderRepository,
        IOrderMappingService orderMappingService,
        IServiceChargeRepository serviceChargeRepository,
        IOrderManagementAuthorizationService orderAuthorizationService,
        IDiscountRepository discountRepository,
        IReservationRepository reservationRepository,
        IReservationService reservationService
    )
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _modifierRepository = modifierRepository;
        _orderRepository = orderRepository;
        _orderMappingService = orderMappingService;
        _serviceChargeRepository = serviceChargeRepository;
        _orderAuthorizationService = orderAuthorizationService;
        _discountRepository = discountRepository;
        _reservationRepository = reservationRepository;
        _reservationService = reservationService;
    }

    public async Task<OrderDTO> CreateOrder(CreateOrderDTO createOrderDTO)
    {
        return await _unitOfWork.ExecuteInTransaction(async () =>
        {
            await _orderAuthorizationService.AuthorizeApplicationUser(createOrderDTO.BusinessId);

            var reservation = createOrderDTO.ReservationId.HasValue
                ? await _reservationRepository.GetWithRelatedData(createOrderDTO.ReservationId.Value)
                : null;

            if (reservation is not null)
            {
                await _reservationService.MarkReservationInProgress(reservation.Id);
            }

            var orderItems = await ReserveOrderItems(createOrderDTO.OrderItems);
            var orderDiscounts = await GetOrderDiscounts(orderItems, createOrderDTO.Discounts, reservation);
            var serviceCharges = await _serviceChargeRepository.GetMany(createOrderDTO.ServiceChargeIds);
            var orderServiceCharges = GetOrderServiceCharges(
                serviceCharges.Cast<IServiceCharge>().ToList(),
                orderItems,
                orderDiscounts,
                reservation
            );

            foreach (var orderItem in orderItems)
            {
                if (orderItem.Product is not null)
                    await _orderAuthorizationService.AuthorizeApplicationUser(orderItem.Product.BusinessId);

                foreach (var orderItemModifier in orderItem.Modifiers)
                {
                    var modifier = await _modifierRepository.Get(orderItemModifier.Id);
                    await _orderAuthorizationService.AuthorizeApplicationUser(modifier.BusinessId);
                }
            }

            foreach (var serviceChargeId in createOrderDTO.ServiceChargeIds)
            {
                var serviceCharge = await _serviceChargeRepository.Get(serviceChargeId);
                await _orderAuthorizationService.AuthorizeApplicationUser(serviceCharge.BusinessId);
            }

            var order = new Order
            {
                Items = orderItems,
                Status = OrderStatus.Open,
                BusinessId = createOrderDTO.BusinessId,
                ServiceCharges = orderServiceCharges,
                Discounts = orderDiscounts,
                ReservationId = reservation?.Id,
            };

            _orderRepository.Add(order);
            await _unitOfWork.SaveChanges();

            return _orderMappingService.MapToOrderDTO(order);
        });
    }

    public async Task<PagedResponseDTO<OrderMinimalDTO>> GetOrders(
        PaginationFilterDTO paginationFilterDTO,
        int businessId
    )
    {
        await _orderAuthorizationService.AuthorizeApplicationUser(businessId);
        var paginationFilter = PaginationFilterFactory.Create(paginationFilterDTO);
        var orders = await _orderRepository.GetWithFilter(paginationFilter, businessId);
        var totalCount = await _orderRepository.GetTotalCount(businessId);
        return _orderMappingService.MapToPagedOrderMinimalDTO(orders, paginationFilter, totalCount);
    }

    public async Task<OrderDTO> GetOrder(int orderId)
    {
        var order = await _orderRepository.GetWithOrderItems(orderId);

        await _orderAuthorizationService.AuthorizeApplicationUser(order.BusinessId);

        return _orderMappingService.MapToOrderDTO(order);
    }

    public async Task<OrderMinimalDTO> GetOrderMinimal(int orderId)
    {
        var order = await _orderRepository.Get(orderId);

        await _orderAuthorizationService.AuthorizeApplicationUser(order.BusinessId);

        return _orderMappingService.MapToOrderMinimalDTO(order);
    }

    public async Task<OrderDTO> UpdateOrder(int orderId, UpdateOrderDTO updateOrderDTO)
    {
        var order = await _orderRepository.GetWithOrderItems(orderId);

        await _orderAuthorizationService.AuthorizeApplicationUser(order.BusinessId);

        if (order.Status != OrderStatus.Open)
        {
            throw new ValidationException(new CannotModifyNonOpenOrderErrorMessage());
        }

        if (updateOrderDTO.OrderItems is not null)
        {
            await ReturnOrderItems(order.Items);
            order.Items = await ReserveOrderItems(updateOrderDTO.OrderItems);

            // Recalculate order discounts
            var orderDiscounts = order
                .Discounts.Select(d => new CreateOrderDiscountDTO
                {
                    Amount = d.Amount,
                    PricingStrategy = d.PricingStrategy,
                })
                .ToList();
            order.Discounts = await GetOrderDiscounts(order.Items, orderDiscounts, order.Reservation);

            // Recalculate order service charges
            order.ServiceCharges = GetOrderServiceCharges(
                order.ServiceCharges.Cast<IServiceCharge>().ToList(),
                order.Items,
                order.Discounts,
                order.Reservation
            );
        }

        if (updateOrderDTO.Discounts is not null)
        {
            order.Discounts = await GetOrderDiscounts(order.Items, updateOrderDTO.Discounts, order.Reservation);
        }

        if (updateOrderDTO.ServiceChargeIds is not null)
        {
            var serviceCharges = await _serviceChargeRepository.GetMany(updateOrderDTO.ServiceChargeIds);
            order.ServiceCharges = GetOrderServiceCharges(
                serviceCharges.Cast<IServiceCharge>().ToList(),
                order.Items,
                order.Discounts,
                order.Reservation
            );
        }

        await _unitOfWork.SaveChanges();

        return _orderMappingService.MapToOrderDTO(order);
    }

    public async Task CancelOrder(int orderId)
    {
        var order = await _orderRepository.GetWithOrderItems(orderId);

        await _orderAuthorizationService.AuthorizeApplicationUser(order.BusinessId);

        if (order.Status != OrderStatus.Open)
        {
            throw new ValidationException(new CannotCancelNonOpenOrderErrorMessage());
        }

        await _unitOfWork.ExecuteInTransaction(async () =>
        {
            await ReturnOrderItems(order.Items);
            order.Status = OrderStatus.Canceled;

            if (order.ReservationId is not null)
            {
                await _reservationService.RevertInProgressReservation(order.ReservationId.Value);
                order.ReservationId = null;
            }

            await _unitOfWork.SaveChanges();
        });
    }

    public async Task<OrderReceiptDTO> GetOrderReceipt(int orderId)
    {
        var order = await _orderRepository.GetWithOrderItems(orderId);

        await _orderAuthorizationService.AuthorizeApplicationUser(order.BusinessId);

        return _orderMappingService.MapToOrderReceiptDTO(order);
    }

    public async Task CompleteOrder(int orderId)
    {
        var order = await _orderRepository.Get(orderId);

        await _orderAuthorizationService.AuthorizeApplicationUser(order.BusinessId);

        if (order.Status != OrderStatus.Open)
        {
            throw new ValidationException(new CannotCompleteNonOpenOrderErrorMessage());
        }

        await _unitOfWork.ExecuteInTransaction(async () =>
        {
            if (order.ReservationId is not null)
            {
                await _reservationService.CompleteReservation(order.ReservationId.Value);
            }

            order.Status = OrderStatus.Completed;
            await _unitOfWork.SaveChanges();
        });
    }

    public async Task CloseOrder(int orderId)
    {
        var order = await _orderRepository.Get(orderId);

        await _orderAuthorizationService.AuthorizeApplicationUser(order.BusinessId);

        if (order.Status != OrderStatus.Completed)
        {
            throw new ValidationException(new CannotCloseNonCompletedOrderErrorMessage());
        }

        order.Status = OrderStatus.Closed;
        await _unitOfWork.SaveChanges();
    }

    public async Task MarkRefunded(int orderId)
    {
        var order = await _orderRepository.Get(orderId);
        await _orderAuthorizationService.AuthorizeApplicationUser(order.BusinessId);

        if (order.Status != OrderStatus.Closed)
        {
            throw new ValidationException(new NonClosedOrderCannotBeRefundedErrorMessage());
        }

        order.Status = OrderStatus.Refunded;
        await _unitOfWork.SaveChanges();
    }

    private async Task<List<OrderItem>> ReserveOrderItems(List<CreateOrUpdateOrderItemDTO> createOrderItemDTOs)
    {
        var productIds = createOrderItemDTOs.Select(x => x.ProductId);
        var products = await _productRepository.GetManyWithRelatedData(productIds);

        List<OrderItem> orderItems = [];
        foreach (var createOrderItemDTO in createOrderItemDTOs)
        {
            var orderItem = ReserveOrderProduct(createOrderItemDTO, products);
            orderItem.Discounts = GetOrderItemDiscounts(orderItem, createOrderItemDTO.Discounts);
            orderItem.Taxes = GetOrderItemTaxes(orderItem);
            orderItems.Add(orderItem);
        }

        return orderItems;
    }

    private static OrderItem ReserveOrderProduct(CreateOrUpdateOrderItemDTO createOrderItemDTO, List<Product> products)
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
        var modifiers = ReserveOrderProductModifiers(createOrderItemDTO, product);

        return new OrderItem
        {
            Name = product.Name,
            Quantity = createOrderItemDTO.Quantity,
            Product = product,
            BaseUnitPrice = product.Price,
            Taxes = [],
            Modifiers = modifiers,
            Discounts = [],
        };
    }

    private static List<OrderItemModifier> ReserveOrderProductModifiers(
        CreateOrUpdateOrderItemDTO createOrderItemDTO,
        Product product
    )
    {
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

            var orderItemModifier = new OrderItemModifier
            {
                ModifierId = modifier.Id,
                Name = modifier.Name,
                Price = modifier.Price,
            };

            modifier.Stock -= createOrderItemDTO.Quantity;
            orderItemModifiers.Add(orderItemModifier);
        }

        return orderItemModifiers;
    }

    private static List<OrderItemTax> GetOrderItemTaxes(OrderItem orderItem)
    {
        var modifiersPrice = orderItem.Modifiers.Sum(x => x.Price);
        var discounts = orderItem.Discounts.Sum(d => d.AppliedAmount);
        var price = (orderItem.BaseUnitPrice + modifiersPrice) * orderItem.Quantity - discounts;

        return orderItem
            .Product!.Taxes.Select(t =>
            {
                var appliedAmount = t.GetAmountToApply(price);
                price += appliedAmount;
                return new OrderItemTax
                {
                    AppliedAmount = appliedAmount,
                    Rate = t.Rate,
                    Name = t.Name,
                };
            })
            .ToList();
    }

    private static List<OrderItemDiscount> GetOrderItemDiscounts(
        OrderItem orderItem,
        List<CreateOrderDiscountDTO> createOrderDiscountDTOs
    )
    {
        var modifiersPrice = orderItem.Modifiers.Sum(x => x.Price);
        var price = (orderItem.BaseUnitPrice + modifiersPrice) * orderItem.Quantity;

        var predefinedDiscounts = orderItem
            .Product!.Discounts.OrderBy(d => d.CreatedAt)
            .Select(d =>
            {
                var appliedAmount = d.GetAmountToApply(price);
                price -= appliedAmount;
                return new OrderItemDiscount
                {
                    Amount = d.Amount,
                    PricingStrategy = d.PricingStrategy,
                    AppliedAmount = appliedAmount,
                    Type = OrderDiscountType.Predefined,
                };
            })
            .ToList();

        var flexibleDiscounts = createOrderDiscountDTOs
            .Select(d =>
            {
                var appliedAmount = d.GetAmountToApply(price);
                price -= appliedAmount;
                return new OrderItemDiscount
                {
                    Amount = d.Amount,
                    PricingStrategy = d.PricingStrategy,
                    AppliedAmount = appliedAmount,
                    Type = OrderDiscountType.Flexible,
                };
            })
            .ToList();

        return predefinedDiscounts.Concat(flexibleDiscounts).ToList();
    }

    private async Task<List<OrderDiscount>> GetOrderDiscounts(
        List<OrderItem> orderItems,
        List<CreateOrderDiscountDTO> createOrderDiscountDTOs,
        Reservation? reservation
    )
    {
        var orderDiscounts = await _discountRepository.GetOrderDiscounts();
        var price = GetOrderItemsPrice(orderItems, reservation);

        var predefinedDiscounts = orderDiscounts
            .OrderBy(d => d.CreatedAt)
            .Select(d =>
            {
                var appliedAmount = d.GetAmountToApply(price);
                price -= appliedAmount;
                return new OrderDiscount
                {
                    Type = OrderDiscountType.Predefined,
                    Amount = d.Amount,
                    PricingStrategy = d.PricingStrategy,
                    AppliedAmount = appliedAmount,
                };
            })
            .ToList();

        var flexibleDiscounts = createOrderDiscountDTOs
            .Select(d =>
            {
                var appliedAmount = d.GetAmountToApply(price);
                price -= appliedAmount;
                return new OrderDiscount
                {
                    Type = OrderDiscountType.Flexible,
                    Amount = d.Amount,
                    PricingStrategy = d.PricingStrategy,
                    AppliedAmount = appliedAmount,
                };
            })
            .ToList();

        return predefinedDiscounts.Concat(flexibleDiscounts).ToList();
    }

    private static List<OrderServiceCharge> GetOrderServiceCharges(
        List<IServiceCharge> serviceCharges,
        List<OrderItem> orderItems,
        List<OrderDiscount> orderDiscounts,
        Reservation? reservation
    )
    {
        var orderDiscount = orderDiscounts.Sum(d => d.AppliedAmount);
        var price = GetOrderItemsPrice(orderItems, reservation) - orderDiscount;

        return serviceCharges
            .OrderBy(c => c.PricingStrategy)
            .Select(c =>
            {
                var appliedAmount = c.GetAmountToApply(price);
                price += appliedAmount;

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

    private static decimal GetOrderItemsPrice(List<OrderItem> orderItems, Reservation? reservation)
    {
        var itemsPrice = orderItems.Sum(i =>
        {
            var productPrice = (i.BaseUnitPrice + i.Modifiers.Sum(m => m.Price)) * i.Quantity;
            var discounts = i.Discounts.Sum(d => d.AppliedAmount);
            var taxes = i.Taxes.Sum(t => t.AppliedAmount);
            return productPrice - discounts + taxes;
        });

        return itemsPrice + (reservation?.Price ?? 0m);
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
