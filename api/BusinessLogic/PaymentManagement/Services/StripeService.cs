using PointOfSale.BusinessLogic.PaymentManagement.DTOs;
using PointOfSale.BusinessLogic.PaymentManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.ErrorMessages;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.Models.PaymentManagement.Enums;
using Stripe;

namespace PointOfSale.BusinessLogic.PaymentManagement.Services;

public class StripeService : IStripeService
{
    private readonly PaymentIntentService _paymentIntentService;

    public StripeService()
    {
        _paymentIntentService = new PaymentIntentService();
    }

    public async Task<PaymentIntentDTO> CreatePaymentIntent(CreatePaymentIntentDTO paymentIntentDTO)
    {
        var paymentIntentOptions = new PaymentIntentCreateOptions
        {
            Amount = (long)((paymentIntentDTO.PaymentAmount + paymentIntentDTO.TipAmount) * 100m),
            Currency = "eur",
            PaymentMethodTypes = ["card"],
            Description = $"Payment for order #{paymentIntentDTO.OrderId}",
        };

        try
        {
            var paymentIntent = await _paymentIntentService.CreateAsync(paymentIntentOptions);

            return new PaymentIntentDTO
            {
                PaymentIntentId = paymentIntent.Id,
                ClientSecret = paymentIntent.ClientSecret,
            };
        }
        catch (StripeException ex)
        {
            throw new ValidationException(new TextErrorMessage(ex.StripeError.Message));
        }
    }

    public async Task<PaymentStatus> GetPaymentIntentStatus(string paymentId)
    {
        var paymentIntent = await _paymentIntentService.GetAsync(paymentId);
        return paymentIntent.Status switch
        {
            "succeeded" => PaymentStatus.Succeeded,
            "canceled" => PaymentStatus.Canceled,
            _ => PaymentStatus.Pending,
        };
    }

    public async Task CancelPaymentIntent(string paymentId)
    {
        await _paymentIntentService.CancelAsync(paymentId);
    }
}
