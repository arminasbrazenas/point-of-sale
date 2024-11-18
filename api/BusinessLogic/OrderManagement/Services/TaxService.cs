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
    private readonly ITaxValidationService _taxValidationService;

    public TaxService(
        IUnitOfWork unitOfWork,
        ITaxRepository taxRepository,
        ITaxMappingService taxMappingService,
        ITaxValidationService taxValidationService
    )
    {
        _unitOfWork = unitOfWork;
        _taxRepository = taxRepository;
        _taxMappingService = taxMappingService;
        _taxValidationService = taxValidationService;
    }

    public async Task<TaxDTO> CreateTax(CreateTaxDTO createTaxDTO)
    {
        var name = await _taxValidationService.ValidateName(createTaxDTO.Name);
        var rate = _taxValidationService.ValidateRate(createTaxDTO.Rate);

        var tax = new Tax { Name = name, Rate = rate };

        _taxRepository.Add(tax);
        await _unitOfWork.SaveChanges();

        return _taxMappingService.MapToTaxDTO(tax);
    }

    public async Task<TaxDTO> UpdateTax(int taxId, UpdateTaxDTO updateTaxDTO)
    {
        var tax = await _taxRepository.Get(taxId);

        if (updateTaxDTO.Name is not null)
        {
            tax.Name = await _taxValidationService.ValidateName(updateTaxDTO.Name);
        }

        if (updateTaxDTO.Rate.HasValue)
        {
            tax.Rate = _taxValidationService.ValidateRate(updateTaxDTO.Rate.Value);
        }

        _taxRepository.Update(tax);
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
        var totalCount = await _taxRepository.GetTotalCount();
        return _taxMappingService.MapToPagedTaxDTO(taxes, paginationFilter, totalCount);
    }

    public async Task DeleteTax(int taxId)
    {
        await _taxRepository.Delete(taxId);
    }
}
