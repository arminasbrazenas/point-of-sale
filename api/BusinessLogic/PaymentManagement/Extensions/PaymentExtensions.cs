using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.Models.PaymentManagement.Entities;

namespace PointOfSale.BusinessLogic.PaymentManagement.Extensions;

public static class PaymentExtensions
{
    public static decimal GetPaidAmount(this List<Payment> payments)
    {
        return payments.Sum(payment => payment.Amount);
    }

    public static decimal GetUnpaidAmount(this List<Payment> payments, OrderDTO orderDTO)
    {
        return orderDTO.TotalPrice - payments.GetPaidAmount();
    }
}
