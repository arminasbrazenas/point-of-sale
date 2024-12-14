using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.Models.PaymentManagement.Entities;
using PointOfSale.Models.PaymentManagement.Enums;

namespace PointOfSale.BusinessLogic.PaymentManagement.Extensions;

public static class PaymentExtensions
{
    public static decimal GetPaidAmount(this List<Payment> payments)
    {
        return payments.Where(p => p.Status == PaymentStatus.Succeeded).Sum(p => p.Amount);
    }

    public static decimal GetUnpaidAmount(this List<Payment> payments, OrderDTO orderDTO)
    {
        return orderDTO.TotalPrice - payments.GetPaidAmount();
    }
}
