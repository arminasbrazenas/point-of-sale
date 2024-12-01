using PointOfSale.Models.BusinessManagement.Entities;

namespace PointOfSale.BusinessLogic.BusinessManagement.Interfaces;

public interface IUserService{
    public Task<User> GetUser(int userId);
}
