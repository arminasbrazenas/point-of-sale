using PointOfSale.BusinessLogic.PaymentManagement.DTOs;

namespace PointOfSale.BusinessLogic.PaymentManagement.Interfaces;

public interface IPaymentService
{
    Task<CashPaymentDTO> PayByCash(CreatePaymentDTO createPaymentDTO);
    Task<OrderPaymentsDTO> GetOrderPayments(int orderId);
    Task CompleteOrderPayments(CompleteOrderPaymentsDTO completeOrderPaymentsDTO);
}
