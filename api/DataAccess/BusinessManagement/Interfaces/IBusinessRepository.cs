using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.BusinessManagement.Entities;

namespace PointOfSale.DataAccess.BusinessManagement.Interfaces;

public interface IBusinessRepository : IRepositoryBase<Business, int> { }
