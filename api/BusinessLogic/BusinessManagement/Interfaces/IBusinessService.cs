using PointOfSale.BusinessLogic.BusinessManagement.DTOs;

namespace PointOfSale.BusinessLogic.BusinessManagement.Interfaces;

public interface IBusinessService
{
    Task<BusinessDTO> CreateBusiness(CreateBusinessDTO createBusinessDTO);
    Task<List<BusinessDTO>> GetBusinesses();
    Task<BusinessDTO> GetBusiness(int businessId);
    Task<BusinessDTO> UpdateBusiness(int businessId, UpdateBusinessDTO updateBusinessDTO);
    Task DeleteBusiness(int businessId);
}
