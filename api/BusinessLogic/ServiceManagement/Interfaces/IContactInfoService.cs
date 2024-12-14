using PointOfSale.BusinessLogic.ServiceManagement.DTOs;

namespace PointOfSale.BusinessLogic.ServiceManagement.Interfaces;

public interface IContactInfoService
{
    public Task<ContactInfoDTO> CreateContactInfo(CreateContactInfoDTO createContactInfoDto);
    public Task<ContactInfoDTO> UpdateContactInfo(int contactInfoId, UpdateContactInfoDTO updateContactInfoDto);
    public Task DeleteContactInfo(int contactInfoId);
    public Task<ContactInfoDTO> GetContactInfo(int contactInfoId);
}