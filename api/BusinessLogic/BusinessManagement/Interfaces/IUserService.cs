using PointOfSale.DataAccess.BusinessManagement;
using PointOfSale.Models.BusinessManagement.Entities;

namespace PointOfSale.BusinessLogic.BusinessManagement.Interfaces;

public interface IUserService
{
    public Task<ApplicationUser> GetUser(int userId);
}
