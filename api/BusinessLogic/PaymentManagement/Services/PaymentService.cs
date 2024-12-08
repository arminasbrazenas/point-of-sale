using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.PaymentManagement.DTOs;
using PointOfSale.BusinessLogic.PaymentManagement.Extensions;
using PointOfSale.BusinessLogic.PaymentManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.DataAccess.PaymentManagement.ErrorMessages;
using PointOfSale.DataAccess.PaymentManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Enums;
using PointOfSale.Models.PaymentManagement.Entities;
using PointOfSale.Models.PaymentManagement.Enums;

namespace PointOfSale.BusinessLogic.PaymentManagement.Services;

public class PaymentService : IPaymentService
{
    private readonly IOrderService _orderService;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentMappingService _paymentMappingService;

    public PaymentService(
        IOrderService orderService,
        IPaymentRepository paymentRepository,
        IUnitOfWork unitOfWork,
        IPaymentMappingService paymentMappingService
    )
    {
        _orderService = orderService;
        _paymentRepository = paymentRepository;
        _unitOfWork = unitOfWork;
        _paymentMappingService = paymentMappingService;
    }

    public async Task<CashPaymentDTO> PayByCash(CreatePaymentDTO createPaymentDTO)
    {
        var order = await _orderService.GetOrder(createPaymentDTO.OrderId);
        ValidateOrderIsCompleted(order);
        await ValidatePaymentAmount(order, createPaymentDTO);

        var payment = new CashPayment
        {
            OrderId = createPaymentDTO.OrderId,
            Method = PaymentMethod.Cash,
            Status = PaymentStatus.Confirmed,
            Amount = createPaymentDTO.PaymentAmount,
        };

        _paymentRepository.Add(payment);
        await _unitOfWork.SaveChanges();

        return _paymentMappingService.MapToCashPaymentDTO(payment);
    }

    public async Task<OrderPaymentsDTO> GetOrderPayments(int orderId)
    {
        var order = await _orderService.GetOrder(orderId);
        return await GetOrderPayments(order);
    }

    public async Task CompleteOrderPayments(CompleteOrderPaymentsDTO completeOrderPaymentsDTO)
    {
        var order = await _orderService.GetOrder(completeOrderPaymentsDTO.OrderId);

        ValidateOrderIsCompleted(order);
        await ValidateOrderIsFullyPaid(order);

        await _orderService.CloseOrder(completeOrderPaymentsDTO.OrderId);
    }

    private async Task<OrderPaymentsDTO> GetOrderPayments(OrderDTO order)
    {
        var orderPayments = await _paymentRepository.GetOrderPayments(order.Id);
        return _paymentMappingService.MapToOrderPaymentsDTO(order, orderPayments);
    }

    private async Task ValidateOrderIsFullyPaid(OrderDTO order)
    {
        var orderPayments = await GetOrderPayments(order);
        if (orderPayments.UnpaidAmount > 0.005m)
        {
            throw new ValidationException(new OrderIsNotFullyPaidErrorMessage());
        }
    }

    private static void ValidateOrderIsCompleted(OrderDTO order)
    {
        if (order.Status != OrderStatus.Completed)
        {
            throw new ValidationException(new CannotPayForNonCompletedOrderErrorMessage());
        }
    }

    private async Task ValidatePaymentAmount(OrderDTO order, CreatePaymentDTO createPaymentDTO)
    {
        if (createPaymentDTO.PaymentAmount <= 0m)
        {
            throw new ValidationException(new PaymentAmountMustBePositiveErrorMessage());
        }

        var orderPayments = await _paymentRepository.GetOrderPayments(order.Id);
        var unpaidAmount = orderPayments.GetUnpaidAmount(order);
        if (createPaymentDTO.PaymentAmount - unpaidAmount > 0.005m)
        {
            throw new ValidationException(new PaymentAmountExceedsOrderPriceErrorMessage());
        }
    }
}
