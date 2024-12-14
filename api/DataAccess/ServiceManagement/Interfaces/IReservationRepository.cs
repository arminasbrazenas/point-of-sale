using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.ServiceManagement.Entities;

namespace PointOfSale.DataAccess.ServiceManagement.Interfaces;

public interface IReservationRepository : IRepositoryBase<Reservation, int>
{
    public List<int> GetServiceResourceIdsByTime(DateTime start, DateTime end);
}