using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.PaymentManagement.DTOs;
using PointOfSale.BusinessLogic.PaymentManagement.Extensions;
using PointOfSale.BusinessLogic.PaymentManagement.Interfaces;
using PointOfSale.Models.PaymentManagement.Entities;
using PointOfSale.Models.PaymentManagement.Enums;

namespace PointOfSale.BusinessLogic.PaymentManagement.Services;

public class PaymentMappingService : IPaymentMappingService
{
    public CashPaymentDTO MapToCashPaymentDTO(CashPayment payment)
    {
        return new CashPaymentDTO
        {
            Id = payment.Id,
            Amount = payment.Amount,
            Method = PaymentMethod.Cash,
        };
    }

    public OrderPaymentsDTO MapToOrderPaymentsDTO(OrderDTO order, List<Payment> payments)
    {
        return new OrderPaymentsDTO
        {
            Payments = payments.Select(MapToPaymentDTO).ToList(),
            PaidAmount = payments.GetPaidAmount(),
            UnpaidAmount = payments.GetUnpaidAmount(order),
        };
    }

    private PaymentDTO MapToPaymentDTO(Payment payment)
    {
        return new PaymentDTO
        {
            Id = payment.Id,
            Amount = payment.Amount,
            Method = payment.Method,
        };
    }
}
