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
    private readonly ITipRepository _tipRepository;
    private readonly IStripeService _stripeService;
    private readonly IOrderManagementAuthorizationService _orderManagementAuthorizationService;

    public PaymentService(
        IOrderService orderService,
        IPaymentRepository paymentRepository,
        IUnitOfWork unitOfWork,
        IPaymentMappingService paymentMappingService,
        IGiftCardService giftCardService,
        ITipRepository tipRepository,
        IStripeService stripeService,
        IOrderManagementAuthorizationService orderManagementAuthorizationService
    )
    {
        _orderService = orderService;
        _paymentRepository = paymentRepository;
        _unitOfWork = unitOfWork;
        _paymentMappingService = paymentMappingService;
        _giftCardService = giftCardService;
        _tipRepository = tipRepository;
        _stripeService = stripeService;
        _orderManagementAuthorizationService = orderManagementAuthorizationService;
    }

    public async Task<CashPaymentDTO> PayByCash(PayByCashDTO payByCashDTO)
    {
        await _orderManagementAuthorizationService.AuthorizeApplicationUser(payByCashDTO.BusinessId);

        var order = await _orderService.GetOrder(payByCashDTO.OrderId);

        await _orderManagementAuthorizationService.AuthorizeApplicationUser(order.BusinessId);

        ValidateOrderIsCompleted(order);
        await ValidatePaymentAmount(order, payByCashDTO.PaymentAmount);

        var payment = new CashPayment
        {
            OrderId = payByCashDTO.OrderId,
            Method = PaymentMethod.Cash,
            Status = PaymentStatus.Succeeded,
            Amount = payByCashDTO.PaymentAmount,
            BusinessId = payByCashDTO.BusinessId,
            EmployeeId = payByCashDTO.EmployeeId,
        };

        _paymentRepository.Add(payment);
        await _unitOfWork.SaveChanges();

        return _paymentMappingService.MapToCashPaymentDTO(payment);
    }

    public async Task<GiftCardPaymentDTO> PayByGiftCard(PayByGiftCardDTO payByGiftCardDTO)
    {
        await _orderManagementAuthorizationService.AuthorizeApplicationUser(payByGiftCardDTO.BusinessId);

        var order = await _orderService.GetOrder(payByGiftCardDTO.OrderId);

        ValidateOrderIsCompleted(order);

        var giftCard = await _giftCardService.GetUsableGiftCardByCode(payByGiftCardDTO.GiftCardCode, order.BusinessId);
        var payment = new GiftCardPayment
        {
            OrderId = payByGiftCardDTO.OrderId,
            Method = PaymentMethod.GiftCard,
            Status = PaymentStatus.Succeeded,
            Amount = giftCard.Amount,
            GiftCardCode = giftCard.Code,
            BusinessId = payByGiftCardDTO.BusinessId,
            EmployeeId = payByGiftCardDTO.EmployeeId,
        };

        await _unitOfWork.ExecuteInTransaction(async () =>
        {
            _paymentRepository.Add(payment);
            await _giftCardService.MarkGiftCardAsUsed(giftCard.Id);
            await _unitOfWork.SaveChanges();
        });

        return _paymentMappingService.MapToGiftCardPaymentDTO(payment);
    }

    public async Task<PaymentIntentDTO> CreateCardPaymentIntent(CreatePaymentIntentDTO createPaymentIntentDTO)
    {
        await _orderManagementAuthorizationService.AuthorizeApplicationUser(createPaymentIntentDTO.BusinessId);

        var order = await _orderService.GetOrder(createPaymentIntentDTO.OrderId);

        ValidateOrderIsCompleted(order);
        await ValidatePaymentAmount(order, createPaymentIntentDTO.PaymentAmount);

        var paymentIntent = await _stripeService.CreatePaymentIntent(createPaymentIntentDTO);

        var payment = new CardPayment
        {
            OrderId = order.Id,
            Method = PaymentMethod.Card,
            Status = PaymentStatus.Pending,
            Amount = createPaymentIntentDTO.PaymentAmount,
            ExternalId = paymentIntent.PaymentIntentId,
            BusinessId = createPaymentIntentDTO.BusinessId,
            EmployeeId = createPaymentIntentDTO.BusinessId,
        };

        var tip = new Tip
        {
            OrderId = order.Id,
            Amount = createPaymentIntentDTO.TipAmount,
            EmployeeId = createPaymentIntentDTO.EmployeeId,
        };

        _paymentRepository.Add(payment);
        _tipRepository.Add(tip);
        await _unitOfWork.SaveChanges();

        return paymentIntent;
    }

    public async Task ConfirmCardPayment(string paymentIntentId)
    {
        var payment = await _paymentRepository.GetCardPaymentByExternalId(paymentIntentId);

        await _orderManagementAuthorizationService.AuthorizeApplicationUser(payment.BusinessId);

        var stripePaymentStatus = await _stripeService.GetPaymentIntentStatus(paymentIntentId);
        if (stripePaymentStatus == PaymentStatus.Succeeded)
        {
            payment.Status = PaymentStatus.Succeeded;
            await _unitOfWork.SaveChanges();
        }
    }

    public async Task ProcessPendingCardPayments()
    {
        var pendingPayments = await _paymentRepository.GetPendingCardPayments();

        foreach (var pendingPayment in pendingPayments)
        {
            var stripePaymentStatus = await _stripeService.GetPaymentIntentStatus(pendingPayment.ExternalId);
            if (stripePaymentStatus != PaymentStatus.Pending)
            {
                pendingPayment.Status = stripePaymentStatus;
            }
        }

        await _unitOfWork.SaveChanges();
    }

    public async Task CancelPendingOutdatedCardPayments()
    {
        var olderThan = TimeSpan.FromMinutes(15);
        var outdatedPayments = await _paymentRepository.GetPendingCardPaymentsOlderThan(olderThan);

        foreach (var outdatedPayment in outdatedPayments)
        {
            await _stripeService.CancelPaymentIntent(outdatedPayment.ExternalId);
            outdatedPayment.Status = PaymentStatus.Canceled;
        }

        await _unitOfWork.SaveChanges();
    }

    public async Task<OrderPaymentsDTO> GetOrderPayments(int orderId)
    {
        var order = await _orderService.GetOrder(orderId);
        await _orderManagementAuthorizationService.AuthorizeApplicationUser(order.BusinessId);
        return await GetOrderPayments(order);
    }

    public async Task CompleteOrderPayments(CompleteOrderPaymentsDTO completeOrderPaymentsDTO)
    {
        var order = await _orderService.GetOrder(completeOrderPaymentsDTO.OrderId);

        ValidateOrderIsCompleted(order);
        await ValidateOrderIsFullyPaid(order);

        await _orderService.CloseOrder(completeOrderPaymentsDTO.OrderId);
    }

    public async Task<TipDTO> AddTip(AddTipDTO addTipDTO)
    {
        var order = await _orderService.GetOrderMinimal(addTipDTO.OrderId);

        var tip = new Tip
        {
            OrderId = order.Id,
            Amount = addTipDTO.TipAmount,
            EmployeeId = addTipDTO.EmployeeId,
        };

        _tipRepository.Add(tip);
        await _unitOfWork.SaveChanges();

        return _paymentMappingService.MapToTipDTO(tip);
    }

    public async Task<List<TipDTO>> GetTips(int orderId)
    {
        var tips = await _tipRepository.GetOrderTips(orderId);
        return _paymentMappingService.MapToTipDTOs(tips);
    }

    public async Task RefundOrderPayments(RefundOrderPaymentsDTO refundOrderPaymentsDTO)
    {
        await _unitOfWork.ExecuteInTransaction(async () =>
        {
            var payments = await _paymentRepository.GetOrderPayments(refundOrderPaymentsDTO.OrderId);
            if (payments.Any(p => p.Method == PaymentMethod.GiftCard))
            {
                throw new ValidationException(new CannotRefundGiftCardPaymentErrorMessage());
            }

            foreach (var payment in payments)
            {
                payment.Status = payment.Method switch
                {
                    PaymentMethod.Cash => PaymentStatus.Refunded,
                    PaymentMethod.Card => PaymentStatus.RefundInitiated,
                    _ => throw new NotImplementedException(
                        $"Refunding for method '{payment.Method}' is not implemented."
                    ),
                };
            }

            await _orderService.MarkRefunded(refundOrderPaymentsDTO.OrderId);
            await _unitOfWork.SaveChanges();
        });
    }

    public async Task CompletePendingRefunds()
    {
        var initiatedRefunds = await _paymentRepository.GetInitiatedCardRefunds();

        foreach (var payment in initiatedRefunds)
        {
            var completeRefundDTO = new RefundPaymentDTO
            {
                Amount = payment.Amount,
                PaymentIntentId = payment.ExternalId,
            };

            await _stripeService.RefundPayment(completeRefundDTO);

            payment.Status = PaymentStatus.Refunded;
            await _unitOfWork.SaveChanges();
        }
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

    private static void ValidateOrderIsCompleted(OrderMinimalDTO order)
    {
        if (order.Status != OrderStatus.Completed)
        {
            throw new ValidationException(new CannotPayForNonCompletedOrderErrorMessage());
        }
    }

    private async Task ValidatePaymentAmount(OrderDTO order, decimal paymentAmount)
    {
        if (paymentAmount <= 0m)
        {
            throw new ValidationException(new PaymentAmountMustBePositiveErrorMessage());
        }

        var orderPayments = await _paymentRepository.GetOrderPayments(order.Id);
        var unpaidAmount = orderPayments.GetUnpaidAmount(order);
        if (paymentAmount - unpaidAmount > 0.005m)
        {
            throw new ValidationException(new PaymentAmountExceedsOrderPriceErrorMessage());
        }
    }
}
