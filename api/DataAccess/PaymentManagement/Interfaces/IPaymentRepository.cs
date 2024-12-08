using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.PaymentManagement.Entities;

namespace PointOfSale.DataAccess.PaymentManagement.Interfaces;

public interface IPaymentRepository : IRepositoryBase<Payment, int>
{
    Task<List<Payment>> GetOrderPayments(int orderId);
}
