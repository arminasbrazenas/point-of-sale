using PointOfSale.BusinessLogic.BusinessManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.BusinessLogic.BusinessManagement.Interfaces;

public interface IBusinessService
{
    Task<BusinessDTO> CreateBusiness(CreateBusinessDTO createBusinessDTO);
    Task<PagedResponseDTO<BusinessDTO>> GetBusinesses(PaginationFilterDTO paginationFilterDTO);
    Task<BusinessDTO> GetBusiness(int businessId);
    Task<BusinessDTO> UpdateBusiness(int businessId, UpdateBusinessDTO updateBusinessDTO);
    Task DeleteBusiness(int businessId);
}
