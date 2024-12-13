using Microsoft.AspNetCore.Identity;
using PointOfSale.BusinessLogic.BusinessManagement.DTOs;
using PointOfSale.BusinessLogic.BusinessManagement.Interfaces;
using PointOfSale.DataAccess.BusinessManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.ApplicationUserManagement.Entities;
using PointOfSale.Models.BusinessManagement.Entities;

namespace PointOfSale.BusinessLogic.BusinessManagement.Services;

public class BusinessService : IBusinessService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBusinessRepository _businessRepository;
    private readonly IBusinessMappingService _businessMappingService;
    private readonly IBusinessValidationService _businessValidationService;
    private readonly IBusinessAuthorizationService _businessAuthorizationService;
    private readonly UserManager<ApplicationUser> _userManager;

    public BusinessService(
        IUnitOfWork unitOfWork,
        IBusinessRepository businessRepository,
        IBusinessMappingService businessMappingService,
        IBusinessValidationService businessValidationService,
        IBusinessAuthorizationService businessAuthorizationService,
        UserManager<ApplicationUser> userManager
    )
    {
        _unitOfWork = unitOfWork;
        _businessRepository = businessRepository;
        _businessMappingService = businessMappingService;
        _businessValidationService = businessValidationService;
        _businessAuthorizationService = businessAuthorizationService;
        _userManager = userManager;
    }

    public async Task<BusinessDTO> CreateBusiness(CreateBusinessDTO createBusinessDTO)
    {
        await _businessAuthorizationService.AuthorizeBusinessWriteAction();

        await _businessValidationService.ValidateCreateBusinessDTO(createBusinessDTO);

        var business = new Business
        {
            BusinessOwnerId = createBusinessDTO.BusinessOwnerId,
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
        await _businessAuthorizationService.AuthorizeBusinessViewAction(businessId);
        var business = await _businessRepository.Get(businessId);
        return _businessMappingService.MapToBusinessDTO(business);
    }

    public async Task DeleteBusiness(int businessId)
    {
        await _businessAuthorizationService.AuthorizeBusinessWriteAction(businessId);
        await _businessRepository.Delete(businessId);
        await _unitOfWork.SaveChanges();
    }

    public Task<List<BusinessDTO>> GetBusinesses()
    {
        throw new NotImplementedException();
    }

    public async Task<BusinessDTO> UpdateBusiness(int businessId, UpdateBusinessDTO updateBusinessDTO)
    {
        await _businessAuthorizationService.AuthorizeBusinessWriteAction(businessId);
        await _businessValidationService.ValidateUpdateBusinessDTO(updateBusinessDTO);

        var business = await _businessRepository.Get(businessId);

        if (updateBusinessDTO.BusinessOwnerId.HasValue)
        {
            business.BusinessOwner = (
                await _userManager.FindByIdAsync(updateBusinessDTO.BusinessOwnerId.Value.ToString())
            )!;
        }

        if (updateBusinessDTO.Name is not null)
        {
            business.Name = updateBusinessDTO.Name;
        }

        if (updateBusinessDTO.Address is not null)
        {
            business.Address = updateBusinessDTO.Address;
        }

        if (updateBusinessDTO.TelephoneNumber is not null)
        {
            business.TelephoneNumber = updateBusinessDTO.TelephoneNumber;
        }

        await _unitOfWork.SaveChanges();

        return _businessMappingService.MapToBusinessDTO(business);
    }
}
