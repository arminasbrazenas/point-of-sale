using PointOfSale.DataAccess.OrderManagement.Filters;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Interfaces;

public interface IReservationRepository : IRepositoryBase<Reservation, int>
{
    Task<Reservation> GetWithRelatedData(int reservationId);
    Task<List<Reservation>> GetPaged(
        PaginationFilter paginationFilter,
        int businessId,
        ReservationFilter? filter = null
    );
    Task<int> GetTotalCount(int businessId);
}