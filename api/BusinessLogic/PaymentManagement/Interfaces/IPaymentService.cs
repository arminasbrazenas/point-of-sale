using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.PaymentManagement.DTOs;

namespace PointOfSale.BusinessLogic.PaymentManagement.Interfaces;

public interface IPaymentService
{
    Task<CashPaymentDTO> PayByCash(PayByCashDTO payByCashDTO);
    Task<GiftCardPaymentDTO> PayByGiftCard(PayByGiftCardDTO payByGiftCardDTO);
    Task<PaymentIntentDTO> CreateCardPaymentIntent(CreatePaymentIntentDTO createPaymentIntentDTO);
    Task ConfirmCardPayment(string paymentIntentId);
    Task ProcessPendingCardPayments();
    Task CancelPendingOutdatedCardPayments();
    Task<OrderPaymentsDTO> GetOrderPayments(int orderId);
    Task CompleteOrderPayments(CompleteOrderPaymentsDTO completeOrderPaymentsDTO);
    Task<TipDTO> AddTip(AddTipDTO addTipDTO);
    Task<List<TipDTO>> GetTips(int orderId);
    Task RefundOrderPayments(RefundOrderPaymentsDTO refundOrderPaymentsDTO);
    Task CompletePendingRefunds();
}
