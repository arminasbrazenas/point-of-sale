using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class TaxMappingService : ITaxMappingService
{
    public TaxDTO MapToTaxDTO(Tax tax)
    {
        return new TaxDTO
        {
            Id = tax.Id,
            Name = tax.Name,
            Rate = tax.Rate,
        };
    }
}
