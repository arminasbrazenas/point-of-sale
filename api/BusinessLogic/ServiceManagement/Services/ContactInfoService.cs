using PointOfSale.BusinessLogic.ServiceManagement.DTOs;
using PointOfSale.BusinessLogic.ServiceManagement.Interfaces;
using PointOfSale.DataAccess.ServiceManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.ServiceManagement.Entities;

namespace PointOfSale.BusinessLogic.ServiceManagement.Services;

public class ContactInfoService : IContactInfoService
{
    private readonly IContactInfoRepository _contactInfoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IContactInfoValidationService _contactInfoValidationService;
    private readonly IContactInfoMappingService _contactInfoMappingService;

    public ContactInfoService(
        IContactInfoValidationService contactInfoValidationService,
        IContactInfoMappingService contactInfoMappingService,
        IUnitOfWork unitOfWork,
        IContactInfoRepository contactInfoRepository)
    {
        _contactInfoValidationService = contactInfoValidationService;
        _contactInfoMappingService = contactInfoMappingService;
        _unitOfWork = unitOfWork;
        _contactInfoRepository = contactInfoRepository;
    }

    public async Task<ContactInfoDTO> CreateContactInfo(CreateContactInfoDTO createContactInfoDto)
    {
        var firstName = _contactInfoValidationService.ValidateName(createContactInfoDto.FirstName);
        var lastName = _contactInfoValidationService.ValidateName(createContactInfoDto.LastName);
        var phoneNumber = _contactInfoValidationService.ValidatePhoneNumber(createContactInfoDto.PhoneNumber);
        
        var contactInfo = new ContactInfo{FirstName = firstName, LastName = lastName, PhoneNumber = phoneNumber};
        
        _contactInfoRepository.Add(contactInfo);
        await _unitOfWork.SaveChanges();
        
        return _contactInfoMappingService.MapToContactInfoDTO(contactInfo);
    }

    public async Task<ContactInfoDTO> UpdateContactInfo(int contactInfoId, UpdateContactInfoDTO updateContactInfoDto)
    {
        var contactInfo = await _contactInfoRepository.Get(contactInfoId);

        if (updateContactInfoDto.FirstName is not null)
        {
            contactInfo.FirstName = _contactInfoValidationService.ValidateName(updateContactInfoDto.FirstName);
        }

        if (updateContactInfoDto.LastName is not null)
        {
            contactInfo.LastName = _contactInfoValidationService.ValidateName(updateContactInfoDto.LastName);
        }

        if (updateContactInfoDto.PhoneNumber is not null)
        {
            contactInfo.PhoneNumber = _contactInfoValidationService.ValidatePhoneNumber(updateContactInfoDto.PhoneNumber);
        }
        
        _contactInfoRepository.Update(contactInfo);
        await _unitOfWork.SaveChanges();
        
        return _contactInfoMappingService.MapToContactInfoDTO(contactInfo);
    }

    public async Task DeleteContactInfo(int contactInfoId)
    {
        await _contactInfoRepository.Delete(contactInfoId);
    }

    public async Task<ContactInfoDTO> GetContactInfo(int contactInfoId)
    {
        var contactInfo = await _contactInfoRepository.Get(contactInfoId);
        return _contactInfoMappingService.MapToContactInfoDTO(contactInfo);
    }
}