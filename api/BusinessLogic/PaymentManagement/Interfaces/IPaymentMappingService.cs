using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.PaymentManagement.DTOs;
using PointOfSale.Models.PaymentManagement.Entities;

namespace PointOfSale.BusinessLogic.PaymentManagement.Interfaces;

public interface IPaymentMappingService
{
    CashPaymentDTO MapToCashPaymentDTO(CashPayment payment);
    GiftCardPaymentDTO MapToGiftCardPaymentDTO(GiftCardPayment payment);
    OnlinePaymentDTO MapToOnlinePaymentDTO(OnlinePayment payment);
    OrderPaymentsDTO MapToOrderPaymentsDTO(OrderDTO order, List<Payment> payments);
    TipDTO MapToTipDTO(Tip tip);
    List<TipDTO> MapToTipDTOs(List<Tip> tips);
}
