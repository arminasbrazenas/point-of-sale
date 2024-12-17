using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.PaymentManagement.DTOs;
using PointOfSale.BusinessLogic.PaymentManagement.Interfaces;
using PointOfSale.DataAccess.PaymentManagement.Interfaces;
using PointOfSale.DataAccess.PaymentManagement.Repositories;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.Models.PaymentManagement.Entities;
using PointOfSale.Models.PaymentManagement.Enums;
using Stripe;
using Stripe.TestHelpers;
namespace PointOfSale.BusinessLogic.PaymentManagement.Services
{
    public class RefundService : IRefundService
    {
        private readonly IStripeService _stripeService;
        private readonly IRefundRepository _refundRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RefundService(IStripeService stripeService, IRefundRepository refundRepository, IUnitOfWork unitOfWork) {
            _stripeService = stripeService;
            _refundRepository = refundRepository;
            _unitOfWork = unitOfWork;
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

            var refund = new PaymentRefund
            {
                ChargeId = stripeRefund.ChargeId,
                Amount = stripeRefund.Amount / 100m,
                Status = RefundStatus.Succeeded,
                Method = Models.PaymentManagement.Enums.PaymentMethod.Online,
                PaymentIntentId = stripeRefund.PaymentIntentId,
                Reason = stripeRefund.Reason
            };

            _refundRepository.Add(refund);
            await _unitOfWork.SaveChanges();

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
