using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.Api.Controllers;

[ApiController]
[Route("v1/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<ActionResult<OrderDTO>> CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
    {
        var order = await _orderService.CreateOrder(createOrderDTO);
        return Ok(order);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDTO<OrderMinimalDTO>>> GetOrders(
        [FromQuery] PaginationFilterDTO paginationFilterDTO
    )
    {
        var orders = await _orderService.GetOrders(paginationFilterDTO);
        return Ok(orders);
    }

    [HttpGet]
    [Route("{orderId:int}")]
    public async Task<ActionResult<OrderDTO>> GetOrder([FromRoute] int orderId)
    {
        var order = await _orderService.GetOrder(orderId);
        return Ok(order);
    }

    [HttpPatch]
    [Route("{orderId:int}")]
    public async Task<ActionResult<OrderDTO>> UpdateOrder(
        [FromRoute] int orderId,
        [FromBody] UpdateOrderDTO updateOrderDTO
    )
    {
        var order = await _orderService.UpdateOrder(orderId, updateOrderDTO);
        return Ok(order);
    }

    [HttpPost]
    [Route("{orderId:int}/cancel")]
    public async Task<IActionResult> CancelOrder([FromRoute] int orderId)
    {
        await _orderService.CancelOrder(orderId);
        return NoContent();
    }
}