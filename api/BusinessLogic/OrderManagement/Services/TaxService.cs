using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.BusinessLogic.Shared.Factories;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class TaxService : ITaxService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaxRepository _taxRepository;
    private readonly ITaxMappingService _taxMappingService;

    public TaxService(IUnitOfWork unitOfWork, ITaxRepository taxRepository, ITaxMappingService taxMappingService)
    {
        _unitOfWork = unitOfWork;
        _taxRepository = taxRepository;
        _taxMappingService = taxMappingService;
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

        return _taxMappingService.MapToTaxDTO(tax);
    }

    public async Task<TaxDTO> GetTax(int taxId)
    {
        var tax = await _taxRepository.Get(taxId);
        return _taxMappingService.MapToTaxDTO(tax);
    }

    public async Task<PagedResponseDTO<TaxDTO>> GetTaxes(PaginationFilterDTO paginationFilterDTO)
    {
        var paginationFilter = PaginationFilterFactory.Create(paginationFilterDTO);
        var taxes = await _taxRepository.GetPaged(paginationFilter);
        return _taxMappingService.MapToPagedTaxDTO(taxes, paginationFilter);
    }

    public async Task DeleteTax(int taxId)
    {
        await _taxRepository.Delete(taxId);
    }
}
