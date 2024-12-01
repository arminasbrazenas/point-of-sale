using PointOfSale.BusinessLogic.BusinessManagement.DTOs;
using PointOfSale.BusinessLogic.BusinessManagement.Interfaces;
using PointOfSale.DataAccess.BusinessManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.BusinessManagement.Entities;

namespace PointOfSale.BusinessLogic.BusinessManagement.Services;

public class BusinessService : IBusinessService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBusinessRepository _businessRepository;
    private readonly IBusinessMappingService _businessMappingService;
    private readonly IUserService _userService;

    public BusinessService(
        IUnitOfWork unitOfWork,
        IBusinessRepository businessRepository,
        IBusinessMappingService businessMappingService,
        IUserService userService
    )
    {
        _unitOfWork = unitOfWork;
        _businessRepository = businessRepository;
        _businessMappingService = businessMappingService;
        _userService = userService;
    }

    public async Task<BusinessDTO> CreateBusiness(CreateBusinessDTO createBusinessDTO)
    {
        var business = new Business
        {
            BusinessOwner = await _userService.GetUser(createBusinessDTO.BusinessOwnerId),
            Name = createBusinessDTO.Name,
            Address = createBusinessDTO.Address,
            Email = createBusinessDTO.Email,
            TelephoneNumber = createBusinessDTO.TelephoneNumber,
        };

        _businessRepository.Add(business);
        await _unitOfWork.SaveChanges();

        return _businessMappingService.MapToBusinessDTO(business);
    }

    public async Task<BusinessDTO> GetBusiness(int businessId)
    {
        var business = await _businessRepository.Get(businessId);
        return _businessMappingService.MapToBusinessDTO(business);
    }

    public async Task DeleteBusiness(int businessId)
    {
        await _businessRepository.Delete(businessId);
    }

    public Task<List<BusinessDTO>> GetBusinesses()
    {
        throw new NotImplementedException();
    }

    public Task<BusinessDTO> UpdateBusiness(int businessId, UpdateBusinessDTO updateBusinessDTO)
    {
        throw new NotImplementedException();
    }
}
