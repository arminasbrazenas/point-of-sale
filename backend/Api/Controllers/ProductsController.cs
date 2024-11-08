using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.Api.Controllers;

[ApiController]
[Route("v1/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> CreateProduct([FromBody] CreateProductDTO createProductDTO)
    {
        var product = await _productService.CreateProduct(createProductDTO);
        return Ok(product);
    }

    [HttpGet]
    [Route("{productId:int}")]
    public async Task<ActionResult<ProductDTO>> GetProduct([FromRoute] int productId)
    {
        var product = await _productService.GetProduct(productId);
        return Ok(product);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponseDTO<ProductDTO>>> GetProducts(
        [FromQuery] PaginationFilterDTO paginationFilterDTO
    )
    {
        var products = await _productService.GetProducts(paginationFilterDTO);
        return Ok(products);
    }

    [HttpPatch]
    [Route("{productId:int}")]
    public async Task<ActionResult<ProductDTO>> UpdateProduct(
        [FromRoute] int productId,
        [FromBody] UpdateProductDTO updateProductDTO
    )
    {
        var product = await _productService.UpdateProduct(productId, updateProductDTO);
        return Ok(product);
    }

    [HttpDelete]
    [Route("{productId:int}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int productId)
    {
        await _productService.DeleteProduct(productId);
        return Ok();
    }
}
