using PointOfSale.BusinessLogic.Order.DTOs;
using PointOfSale.BusinessLogic.Order.Interfaces;
using PointOfSale.DataAccess.Order.Entities;
using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.BusinessLogic.Order.Services;

public class TaxService : ITaxService
{
    private readonly IUnitOfWork _unitOfWork;

    public TaxService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TaxDTO> CreateTax(CreateTaxDTO createTaxDTO)
    {
        var tax = new Tax
        {
            Name = createTaxDTO.Name,
            Rate = createTaxDTO.Rate
        };
        
        _unitOfWork.Taxes.Add(tax);
        await _unitOfWork.SaveChanges();

        return TaxDTO.FromEntity(tax);
    }
}