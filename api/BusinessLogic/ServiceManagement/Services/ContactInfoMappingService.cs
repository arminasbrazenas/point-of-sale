using PointOfSale.BusinessLogic.ServiceManagement.DTOs;
using PointOfSale.BusinessLogic.ServiceManagement.Interfaces;
using PointOfSale.Models.ServiceManagement.Entities;

namespace PointOfSale.BusinessLogic.ServiceManagement.Services;

public class ContactInfoMappingService : IContactInfoMappingService
{
    public ContactInfoDTO MapToContactInfoDTO(ContactInfo contactInfo)
    {
        return new ContactInfoDTO
        {
            Id = contactInfo.Id,
            FirstName = contactInfo.FirstName,
            LastName = contactInfo.LastName,
            PhoneNumber = contactInfo.PhoneNumber,
        };
    }
}