using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.PaymentManagement.Entities;

namespace PointOfSale.DataAccess.PaymentManagement.Interfaces;

public interface ITipRepository : IRepositoryBase<Tip, int>
{
    Task<List<Tip>> GetOrderTips(int orderId);
}
