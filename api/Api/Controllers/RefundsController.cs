using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.PaymentManagement.DTOs;
using PointOfSale.BusinessLogic.PaymentManagement.Interfaces;

namespace PointOfSale.Api.Controllers;

[ApiController]
[Route("v1/refunds")]
public class RefundsController : ControllerBase
{
    private readonly IRefundService _refundService;

    public RefundsController(IRefundService refundService)
    {
        _refundService = refundService;
    }

    [HttpPost]
    public async Task<ActionResult<RefundResponseDTO>> CreateRefund([FromBody] RefundRequestDTO refundRequest)
    {
        var refundResponse = await _refundService.RefundPaymentAsync(refundRequest);
        return Ok(refundResponse);
    }

    [HttpGet("{refundId}")]
    public async Task<ActionResult<RefundResponseDTO>> GetRefundById([FromRoute] string refundId)
    {
        var refundDetails = await _refundService.GetRefundDetailsAsync(refundId);
        return Ok(refundDetails);
    }
}