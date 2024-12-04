using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace PointOfSale.Models.BusinessManagement.Entities;

public class ApplicationUser : IdentityUser<int>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public ClaimsIdentity GenerateUserIdentity(UserManager<ApplicationUser> manager)
    {
        var userIdentity = new ClaimsIdentity("Identity.Application");

        userIdentity.AddClaim(new Claim(ClaimTypes.GivenName, FirstName));
        userIdentity.AddClaim(new Claim(ClaimTypes.Surname, LastName));

        return userIdentity;
    }
}

public class ApplicationRole : IdentityRole<int>
{
    public ApplicationRole() { }

    public ApplicationRole(string name)
    {
        Name = name;
    }
}

public class ApplicationUserRole : IdentityUserRole<int> { }

public class ApplicationUserClaim : IdentityUserClaim<int> { }

public class ApplicationUserLogin : IdentityUserLogin<int> { }
