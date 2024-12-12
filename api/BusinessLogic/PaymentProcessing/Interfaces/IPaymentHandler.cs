using PointOfSale.BusinessLogic.PaymentProcessing.DTOs;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.PaymentProcessing.Interfaces
{
    public interface IPaymentHandler
    {
        Task ProcessPayment(OrderPayment payment, PaymentDTO paymentDTO);
    }
}
