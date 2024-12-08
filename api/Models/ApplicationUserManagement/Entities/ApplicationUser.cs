using Microsoft.AspNetCore.Identity;
using PointOfSale.Models.BusinessManagement.Entities;

namespace PointOfSale.Models.ApplicationUserManagement.Entities;

public class ApplicationUser : IdentityUser<int>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public Business? Business { get; set; }
}