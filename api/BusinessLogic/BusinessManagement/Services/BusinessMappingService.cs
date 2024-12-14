using PointOfSale.BusinessLogic.BusinessManagement.DTOs;
using PointOfSale.BusinessLogic.BusinessManagement.Interfaces;
using PointOfSale.Models.BusinessManagement.Entities;

namespace PointOfSale.BusinessLogic.BusinessManagement.Services;

public class BusinessMappingService : IBusinessMappingService
{
    public BusinessDTO MapToBusinessDTO(Business business)
    {
        return new BusinessDTO
        {
            Id = business.Id,
            BusinessOwnerId = business.BusinessOwner.Id,
            Name = business.Name,
            Address = business.Address,
            Email = business.Email,
            TelephoneNumber = business.TelephoneNumber,
        };
    }
}
