using PointOfSale.BusinessLogic.PaymentManagement.DTOs;

namespace PointOfSale.BusinessLogic.PaymentManagement.Interfaces;

public interface IPaymentService
{
    Task<CashPaymentDTO> PayByCash(PayByCashDTO payByCashDTO);
    Task<GiftCardPaymentDTO> PayByGiftCard(PayByGiftCardDTO payByGiftCardDTO);
    Task<OrderPaymentsDTO> GetOrderPayments(int orderId);
    Task CompleteOrderPayments(CompleteOrderPaymentsDTO completeOrderPaymentsDTO);
}
