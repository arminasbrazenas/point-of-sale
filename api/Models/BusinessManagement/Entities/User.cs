using Microsoft.AspNetCore.Identity;

namespace PointOfSale.Models.BusinessManagement.Entities;

public class User : IdentityUser<int>
{
    public required string AuthorizationServiceId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}