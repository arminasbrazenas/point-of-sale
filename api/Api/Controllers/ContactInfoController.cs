using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.ServiceManagement.DTOs;
using PointOfSale.BusinessLogic.ServiceManagement.Interfaces;

namespace PointOfSale.Api.Controllers;

[ApiController]
[Route("ContactInfo")]
public class ContactInfoController : ControllerBase
{
    private readonly IContactInfoService _contactInfoService;

    public ContactInfoController(IContactInfoService contactInfoService)
    {
        _contactInfoService = contactInfoService;
    }

    [HttpPost]
    public async Task<ActionResult<ContactInfoDTO>> CreateContactInfo([FromBody] CreateContactInfoDTO createContactInfo)
    {
        var contactInfo = await _contactInfoService.CreateContactInfo(createContactInfo);
        return Ok(contactInfo);
    }
    
    [HttpPatch]
    [Route("{contactInfoId:int}")]
    public async Task<ActionResult<ContactInfoDTO>> UpdateContactInfo([FromRoute] int contactInfoId, [FromBody] UpdateContactInfoDTO updateContactInfo)
    {
        var contactInfo = await _contactInfoService.UpdateContactInfo(contactInfoId, updateContactInfo);
        return Ok(contactInfo);
    }
    
    [HttpGet]
    [Route("{contactInfoId:int}")]
    public async Task<ActionResult<ContactInfoDTO>> GetContactInfo([FromRoute] int contactInfoId)
    {
        var contactInfo = await _contactInfoService.GetContactInfo(contactInfoId);
        return Ok(contactInfo);
    }
    
    [HttpDelete]
    [Route("{contactInfoId:int}")]
    public async Task<ActionResult<ContactInfoDTO>> DeleteContactInfo([FromRoute] int contactInfoId)
    {
        await _contactInfoService.DeleteContactInfo(contactInfoId);
        return NoContent();
    }
}