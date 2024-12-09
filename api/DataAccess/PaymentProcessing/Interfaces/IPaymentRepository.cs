using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.PaymentProcessing.Interfaces
{
	public interface IPaymentRepository : IRepositoryBase<OrderPayment, int>
    {

	}
}