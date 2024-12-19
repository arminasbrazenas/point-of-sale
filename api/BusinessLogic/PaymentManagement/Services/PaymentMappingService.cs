using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.PaymentManagement.DTOs;
using PointOfSale.BusinessLogic.PaymentManagement.Extensions;
using PointOfSale.BusinessLogic.PaymentManagement.Interfaces;
using PointOfSale.Models.PaymentManagement.Entities;
using PointOfSale.Models.PaymentManagement.Enums;

namespace PointOfSale.BusinessLogic.PaymentManagement.Services;

public class PaymentMappingService : IPaymentMappingService
{
    public CashPaymentDTO MapToCashPaymentDTO(CashPayment payment)
    {
        return new CashPaymentDTO
        {
            Id = payment.Id,
            Amount = payment.Amount,
            Method = PaymentMethod.Cash,
            Status = payment.Status,
        };
    }

    public GiftCardPaymentDTO MapToGiftCardPaymentDTO(GiftCardPayment payment)
    {
        return new GiftCardPaymentDTO
        {
            Id = payment.Id,
            Amount = payment.Amount,
            Method = PaymentMethod.GiftCard,
            Status = payment.Status,
            GiftCardCode = payment.GiftCardCode,
        };
    }

    public CardPaymentDTO MapToCardPaymentDTO(CardPayment payment)
    {
        return new CardPaymentDTO
        {
            Id = payment.Id,
            Amount = payment.Amount,
            Status = payment.Status,
            Method = PaymentMethod.Card,
        };
    }

    public OrderPaymentsDTO MapToOrderPaymentsDTO(OrderDTO order, List<Payment> payments)
    {
        return new OrderPaymentsDTO
        {
            Payments = payments.Select(MapToPaymentDTO).ToList(),
            PaidAmount = payments.GetPaidAmount(),
            UnpaidAmount = payments.GetUnpaidAmount(order),
            TotalAmount = order.TotalPrice,
        };
    }

    public TipDTO MapToTipDTO(Tip tip)
    {
        return new TipDTO { Id = tip.Id, Amount = tip.Amount };
    }

    public List<TipDTO> MapToTipDTOs(List<Tip> tips)
    {
        return tips.Select(MapToTipDTO).ToList();
    }

    private PaymentDTO MapToPaymentDTO(Payment payment) =>
        payment.Method switch
        {
            PaymentMethod.Cash => MapToCashPaymentDTO((CashPayment)payment),
            PaymentMethod.GiftCard => MapToGiftCardPaymentDTO((GiftCardPayment)payment),
            PaymentMethod.Card => MapToCardPaymentDTO((CardPayment)payment),
            _ => throw new NotImplementedException($"Payment method {payment.Method} mapping is not implemented."),
        };
}
