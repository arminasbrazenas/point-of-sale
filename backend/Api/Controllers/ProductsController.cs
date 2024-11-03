using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.Order.DTOs;
using PointOfSale.BusinessLogic.Order.Interfaces;

namespace PointOfSale.Api.Controllers;

[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> CreateProduct(CreateProductDTO createProductDTO)
    {
        var product = await _productService.CreateProduct(createProductDTO);
        return Ok(product);
    }
}
