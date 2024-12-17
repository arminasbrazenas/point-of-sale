using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.PaymentManagement.DTOs;
using PointOfSale.BusinessLogic.PaymentManagement.Interfaces;
using Stripe;
using Stripe.TestHelpers;
namespace PointOfSale.BusinessLogic.PaymentManagement.Services
{
    public class RefundService : IRefundService
    {
        private readonly IStripeService _stripeService;
        public RefundService(IStripeService stripeService) {
            _stripeService = stripeService;
        }
        public async Task<RefundResponseDTO> RefundPaymentAsync(RefundRequestDTO refundRequest)
        {
            var refundOptions = new RefundCreateOptions
            {
                Charge = refundRequest.ChargeId,
                PaymentIntent = refundRequest.PaymentIntentId,
                Amount = refundRequest.Amount,
                Reason = refundRequest.Reason
            };

            var stripeRefund = await _stripeService.CreateRefundAsync(refundOptions);

            var refundResponse = new RefundResponseDTO
            {
                RefundId = stripeRefund.Id,
                Status = stripeRefund.Status,
                AmountRefunded = stripeRefund.Amount,
                ChargeId = stripeRefund.Charge,
                PaymentIntentId = stripeRefund.PaymentIntent,
                Reason = stripeRefund.Reason
            };

            return refundResponse;
        }

        public async Task<RefundResponseDTO> GetRefundDetailsAsync(string refundId)
        {
            var stripeRefund = await _stripeService.GetRefundAsync(refundId);

            var refundResponse = new RefundResponseDTO
            {
                RefundId = stripeRefund.Id,
                Status = stripeRefund.Status,
                AmountRefunded = stripeRefund.Amount,
                ChargeId = stripeRefund.Charge,
                PaymentIntentId = stripeRefund.PaymentIntent,
                Reason = stripeRefund.Reason
            };

            return refundResponse;
        }
    }
}
