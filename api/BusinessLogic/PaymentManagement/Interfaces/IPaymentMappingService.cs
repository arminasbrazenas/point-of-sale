using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.PaymentManagement.DTOs;
using PointOfSale.Models.PaymentManagement.Entities;

namespace PointOfSale.BusinessLogic.PaymentManagement.Interfaces;

public interface IPaymentMappingService
{
    CashPaymentDTO MapToCashPaymentDTO(CashPayment payment);
    OrderPaymentsDTO MapToOrderPaymentsDTO(OrderDTO order, List<Payment> payments);
}
