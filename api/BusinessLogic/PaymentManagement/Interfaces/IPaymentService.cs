using PointOfSale.BusinessLogic.PaymentManagement.DTOs;

namespace PointOfSale.BusinessLogic.PaymentManagement.Interfaces;

public interface IPaymentService
{
    Task<CashPaymentDTO> PayByCash(PayByCashDTO payByCashDTO);
    Task<GiftCardPaymentDTO> PayByGiftCard(PayByGiftCardDTO payByGiftCardDTO);
    Task<PaymentIntentDTO> CreateOnlinePaymentIntent(CreatePaymentIntentDTO createPaymentIntentDTO);
    Task ProcessPendingOnlinePayments();
    Task CancelPendingOutdatedOnlinePayments();
    Task<OrderPaymentsDTO> GetOrderPayments(int orderId);
    Task CompleteOrderPayments(CompleteOrderPaymentsDTO completeOrderPaymentsDTO);
    Task<TipDTO> AddTip(AddTipDTO addTipDTO);
    Task<List<TipDTO>> GetTips(int orderId);
}
