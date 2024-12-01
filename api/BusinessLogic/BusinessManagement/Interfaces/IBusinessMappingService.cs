using PointOfSale.BusinessLogic.BusinessManagement.DTOs;
using PointOfSale.Models.BusinessManagement.Entities;

namespace PointOfSale.BusinessLogic.BusinessManagement.Interfaces;

public interface IBusinessMappingService
{
    BusinessDTO MapToBusinessDTO(Business business);
}
