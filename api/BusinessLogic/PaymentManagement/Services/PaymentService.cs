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
    private readonly IGiftCardService _giftCardService;

    public PaymentService(
        IOrderService orderService,
        IPaymentRepository paymentRepository,
        IUnitOfWork unitOfWork,
        IPaymentMappingService paymentMappingService,
        IGiftCardService giftCardService
    )
    {
        _orderService = orderService;
        _paymentRepository = paymentRepository;
        _unitOfWork = unitOfWork;
        _paymentMappingService = paymentMappingService;
        _giftCardService = giftCardService;
    }

    public async Task<CashPaymentDTO> PayByCash(PayByCashDTO payByCashDTO)
    {
        var order = await _orderService.GetOrder(payByCashDTO.OrderId);
        ValidateOrderIsCompleted(order);
        await ValidatePaymentAmount(order, payByCashDTO);

        var payment = new CashPayment
        {
            OrderId = payByCashDTO.OrderId,
            Method = PaymentMethod.Cash,
            Status = PaymentStatus.Confirmed,
            Amount = payByCashDTO.PaymentAmount,
        };

        _paymentRepository.Add(payment);
        await _unitOfWork.SaveChanges();

        return _paymentMappingService.MapToCashPaymentDTO(payment);
    }

    public async Task<GiftCardPaymentDTO> PayByGiftCard(PayByGiftCardDTO payByGiftCardDTO)
    {
        var order = await _orderService.GetOrder(payByGiftCardDTO.OrderId);
        ValidateOrderIsCompleted(order);

        var giftCard = await _giftCardService.GetUsableGiftCardByCode(payByGiftCardDTO.GiftCardCode);
        var payment = new GiftCardPayment
        {
            OrderId = payByGiftCardDTO.OrderId,
            Method = PaymentMethod.GiftCard,
            Status = PaymentStatus.Confirmed,
            Amount = giftCard.Amount,
            GiftCardCode = giftCard.Code,
        };

        await _unitOfWork.ExecuteInTransaction(async () =>
        {
            _paymentRepository.Add(payment);
            await _giftCardService.MarkGiftCardAsUsed(giftCard.Id);
            await _unitOfWork.SaveChanges();
        });

        return _paymentMappingService.MapToGiftCardPaymentDTO(payment);
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

    private async Task ValidatePaymentAmount(OrderDTO order, PayByCashDTO payByCashDTO)
    {
        if (payByCashDTO.PaymentAmount <= 0m)
        {
            throw new ValidationException(new PaymentAmountMustBePositiveErrorMessage());
        }

        var orderPayments = await _paymentRepository.GetOrderPayments(order.Id);
        var unpaidAmount = orderPayments.GetUnpaidAmount(order);
        if (payByCashDTO.PaymentAmount - unpaidAmount > 0.005m)
        {
            throw new ValidationException(new PaymentAmountExceedsOrderPriceErrorMessage());
        }
    }
}
