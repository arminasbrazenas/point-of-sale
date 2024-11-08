using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.DataAccess.OrderManagement;
using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Exceptions;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Validation;

public class ProductValidator : IProductValidator
{
    private readonly IProductRepository _productRepository;
    private readonly ITaxRepository _taxRepository;

    public ProductValidator(IProductRepository productRepository, ITaxRepository taxRepository)
    {
        _productRepository = productRepository;
        _taxRepository = taxRepository;
    }

    public async Task<string> ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ValidationException(new ProductNameEmptyErrorMessage());
        }

        name = name.Trim();
        if (name.Length > Constants.ProductNameMaxLength)
        {
            throw new ValidationException(new ProductNameTooLongErrorMessage(Constants.ProductNameMaxLength));
        }

        // TODO: check by (business id, name)
        var existingProduct = await _productRepository.GetByNameOptional(name);
        if (existingProduct is not null)
        {
            throw new ValidationException(new ProductNameConflictErrorMessage(name));
        }

        return name;
    }

    public decimal ValidatePrice(decimal price)
    {
        if (price < 0m)
        {
            throw new ValidationException(new ProductPriceNegativeErrorMessage());
        }

        return price;
    }

    public int ValidateStock(int stock)
    {
        if (stock < 0)
        {
            throw new ValidationException(new ProductStockNegativeErrorMessage());
        }

        return stock;
    }

    public async Task<List<Tax>> ValidateTaxes(List<int> taxIds)
    {
        if (taxIds.Count == 0)
        {
            return [];
        }

        if (taxIds.Count != taxIds.Distinct().Count())
        {
            throw new ValidationException(new ProductDuplicateTaxErrorMessage());
        }

        var taxes = await _taxRepository.GetMany(taxIds);
        var nonExistentTaxIds = taxIds.Except(taxes.Select(t => t.Id)).ToList();
        if (nonExistentTaxIds.Count > 0)
        {
            throw new ValidationException(new TaxesNotFoundErrorMessage(nonExistentTaxIds));
        }

        return taxes;
    }
}
