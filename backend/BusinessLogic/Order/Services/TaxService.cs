using PointOfSale.BusinessLogic.Order.DTOs;
using PointOfSale.BusinessLogic.Order.Interfaces;
using PointOfSale.DataAccess.Order.Entities;
using PointOfSale.DataAccess.Order.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.BusinessLogic.Order.Services;

public class TaxService : ITaxService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaxRepository _taxRepository;

    public TaxService(IUnitOfWork unitOfWork, ITaxRepository taxRepository)
    {
        _unitOfWork = unitOfWork;
        _taxRepository = taxRepository;
    }

    public async Task<TaxDTO> CreateTax(CreateTaxDTO createTaxDTO)
    {
        // TODO: validation

        var tax = new Tax
        {
            Name = createTaxDTO.Name,
            Rate = createTaxDTO.Rate,
            Products = [],
        };

        _taxRepository.Add(tax);
        await _unitOfWork.SaveChanges();

        return TaxDTO.Create(tax);
    }

    public async Task<TaxDTO> GetTax(int taxId)
    {
        var tax = await _taxRepository.Get(taxId);
        return TaxDTO.Create(tax);
    }

    public async Task DeleteTax(int taxId)
    {
        await _taxRepository.Delete(taxId);
    }
}
