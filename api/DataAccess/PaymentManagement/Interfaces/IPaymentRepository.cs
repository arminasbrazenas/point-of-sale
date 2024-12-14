using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.PaymentManagement.Entities;

namespace PointOfSale.DataAccess.PaymentManagement.Interfaces;

public interface IPaymentRepository : IRepositoryBase<Payment, int>
{
    Task<List<Payment>> GetOrderPayments(int orderId);
    Task<List<OnlinePayment>> GetPendingOnlinePayments();
    Task<List<OnlinePayment>> GetPendingOnlinePaymentsOlderThan(TimeSpan olderThan);
    Task<OnlinePayment> GetOnlinePaymentByExternalId(string externalId);
}
