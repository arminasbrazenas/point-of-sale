using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.ApplicationUserManagement.Entities;

namespace PointOfSale.DataAccess.ApplicationUserManagement.Interfaces;

public interface IRefreshTokenRepository : IRepositoryBase<RefreshToken, int> {
    Task<RefreshToken?> GetRefreshTokenByHash(string hashedToken);
 }
