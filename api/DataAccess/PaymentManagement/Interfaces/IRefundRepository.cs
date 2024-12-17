using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.PaymentManagement.Entities;

namespace PointOfSale.DataAccess.PaymentManagement.Interfaces
{
    public interface IRefundRepository : IRepositoryBase<PaymentRefund, int>
    {
        Task<List<PaymentRefund>> GetRefundsByPaymentIntentIdAsync(string paymentIntentId);
    }
}
