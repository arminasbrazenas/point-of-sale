using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.PaymentManagement.Entities;

namespace PointOfSale.DataAccess.PaymentManagement.Interfaces;

public interface IPaymentRepository : IRepositoryBase<Payment, int>
{
    Task<List<Payment>> GetOrderPayments(int orderId);
    Task<List<CardPayment>> GetPendingCardPayments();
    Task<List<CardPayment>> GetPendingCardPaymentsOlderThan(TimeSpan olderThan);
    Task<CardPayment> GetCardPaymentByExternalId(string externalId);
    Task<List<CardPayment>> GetInitiatedCardRefunds();
}
