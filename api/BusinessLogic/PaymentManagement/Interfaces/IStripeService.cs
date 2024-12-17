using PointOfSale.BusinessLogic.PaymentManagement.DTOs;
using PointOfSale.Models.PaymentManagement.Enums;
using Stripe;

namespace PointOfSale.BusinessLogic.PaymentManagement.Interfaces;

public interface IStripeService
{
    Task<PaymentIntentDTO> CreatePaymentIntent(CreatePaymentIntentDTO paymentIntentDTO);
    Task<PaymentStatus> GetPaymentIntentStatus(string paymentId);
    Task CancelPaymentIntent(string paymentId);
    Task<Refund> CreateRefundAsync(RefundCreateOptions refundCreateOptions);
    Task<Refund> GetRefundAsync(string refundId);
}
