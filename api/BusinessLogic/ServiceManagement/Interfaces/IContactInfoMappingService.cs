using PointOfSale.BusinessLogic.ServiceManagement.DTOs;
using PointOfSale.Models.ServiceManagement.Entities;

namespace PointOfSale.BusinessLogic.ServiceManagement.Interfaces;

public interface IContactInfoMappingService
{
    public ContactInfoDTO MapToContactInfoDTO(ContactInfo contactInfo);
}