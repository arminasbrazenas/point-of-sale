using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Interfaces;

public interface IReservationRepository : IRepositoryBase<Reservation, int>
{
    Task<List<Reservation>> GetPaged(PaginationFilter paginationFilter);
}