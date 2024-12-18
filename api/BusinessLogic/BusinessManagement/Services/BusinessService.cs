using Microsoft.AspNetCore.Identity;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;
using PointOfSale.BusinessLogic.BusinessManagement.DTOs;
using PointOfSale.BusinessLogic.BusinessManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.BusinessLogic.Shared.Factories;
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
    private readonly IContactInfoValidationService _contactInfoValidationService;

    public BusinessService(
        IUnitOfWork unitOfWork,
        IBusinessRepository businessRepository,
        IBusinessMappingService businessMappingService,
        IBusinessValidationService businessValidationService,
        IBusinessAuthorizationService businessAuthorizationService,
        UserManager<ApplicationUser> userManager,
        IContactInfoValidationService contactInfoValidationService
    )
    {
        _unitOfWork = unitOfWork;
        _businessRepository = businessRepository;
        _businessMappingService = businessMappingService;
        _businessValidationService = businessValidationService;
        _businessAuthorizationService = businessAuthorizationService;
        _userManager = userManager;
        _contactInfoValidationService = contactInfoValidationService;
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
            TelephoneNumber = createBusinessDTO.PhoneNumber,
            WorkingHours = new BusinessWorkingHours
            {
                Start = new TimeOnly(createBusinessDTO.StartHour, createBusinessDTO.StartMinute),
                End = new TimeOnly(createBusinessDTO.EndHour, createBusinessDTO.EndMinute),
            },
        };

        _businessRepository.Add(business);

        await _unitOfWork.SaveChanges();

        var businessOwner = await _userManager.FindByIdAsync(createBusinessDTO.BusinessOwnerId.ToString());
        businessOwner!.OwnedBusiness = business;
        await _userManager.UpdateAsync(businessOwner);

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

    public async Task<PagedResponseDTO<BusinessDTO>> GetBusinesses(PaginationFilterDTO paginationFilterDTO)
    {
        var paginationFilter = PaginationFilterFactory.Create(paginationFilterDTO);
        var businesses = await _businessRepository.GetPagedBusiness(paginationFilter);
        var totalCount = await _businessRepository.GetTotalCount();
        return _businessMappingService.MapToPagedBusinessDTO(businesses, paginationFilter, totalCount);
    }

    public async Task<BusinessDTO> UpdateBusiness(int businessId, UpdateBusinessDTO updateBusinessDTO)
    {
        await _businessAuthorizationService.AuthorizeBusinessWriteAction(businessId);

        var business = await _businessRepository.Get(businessId);

        if (updateBusinessDTO.Name is not null)
        {
            business.Name = updateBusinessDTO.Name;
        }

        if (updateBusinessDTO.Address is not null)
        {
            business.Address = updateBusinessDTO.Address;
        }

        if (updateBusinessDTO.PhoneNumber is not null)
        {
            _contactInfoValidationService.ValidatePhoneNumber(updateBusinessDTO.PhoneNumber);
            business.TelephoneNumber = updateBusinessDTO.PhoneNumber;
        }

        if (updateBusinessDTO.Email is not null)
        {
            _contactInfoValidationService.ValidateEmail(updateBusinessDTO.Email);
            business.Email = updateBusinessDTO.Email;
        }

        var startHour = business.WorkingHours.Start.Hour;
        var startMinute = business.WorkingHours.Start.Minute;

        var endHour = business.WorkingHours.End.Hour;
        var endMinute = business.WorkingHours.End.Minute;

        if (updateBusinessDTO.StartHour.HasValue)
        {
            startHour = updateBusinessDTO.StartHour.Value;
        }

        if (updateBusinessDTO.StartMinute.HasValue)
        {
            startMinute = updateBusinessDTO.StartMinute.Value;
        }

        if (updateBusinessDTO.EndHour.HasValue)
        {
            endHour = updateBusinessDTO.EndHour.Value;
        }

        if (updateBusinessDTO.EndMinute.HasValue)
        {
            endMinute = updateBusinessDTO.EndMinute.Value;
        }

        _businessValidationService.ValidateTime(startHour, startMinute, endHour, endMinute);

        business.WorkingHours.Start = new TimeOnly(startHour, startMinute);
        business.WorkingHours.End = new TimeOnly(endHour, endMinute);

        await _unitOfWork.SaveChanges();

        return _businessMappingService.MapToBusinessDTO(business);
    }
}
