using PointOfSale.BusinessLogic.PaymentProcessing.DTOs;
using PointOfSale.BusinessLogic.PaymentProcessing.Interfaces;
using PointOfSale.BusinessLogic.OrderManagement.Utilities;
using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.OrderManagement.Enums;
using PointOfSale.Models.PaymentProcessing.Enums;

namespace PointOfSale.BusinessLogic.PaymentProcessing.Services
{
    public class CashPaymentHandler : IPaymentHandler
    {
        public async Task ProcessPayment(OrderPayment orderPayment, PaymentDTO paymentDTO)
        {
            var order = orderPayment.Order;
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            var totalDue = PriceUtility.CalculateTotalDue(order.ServiceCharges);

            orderPayment.TotalPaid += paymentDTO.PayAmount;

            if (orderPayment.TotalPaid >= totalDue)
            {
                orderPayment.PaymentStatus = PaymentStatus.Confirmed;
                order.Status = OrderStatus.Closed;
            }
            else
            {
                orderPayment.PaymentStatus = PaymentStatus.Pending;
                order.Status = OrderStatus.Open;
            }

            await Task.CompletedTask;
        }
    }
}
