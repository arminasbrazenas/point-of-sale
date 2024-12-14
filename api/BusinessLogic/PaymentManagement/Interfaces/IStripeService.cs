using PointOfSale.BusinessLogic.PaymentManagement.DTOs;
using PointOfSale.Models.PaymentManagement.Enums;

namespace PointOfSale.BusinessLogic.PaymentManagement.Interfaces;

public interface IStripeService
{
    Task<PaymentIntentDTO> CreatePaymentIntent(CreatePaymentIntentDTO paymentIntentDTO);
    Task<PaymentStatus> GetPaymentIntentStatus(string paymentId);
    Task CancelPaymentIntent(string paymentId);
}
