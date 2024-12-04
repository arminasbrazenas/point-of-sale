using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PointOfSale.Models.BusinessManagement.Entities;

namespace PointOfSale.DataAccess.BusinessManagement;

public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, int>
{
    public ApplicationUserStore(ApplicationDbContext context)
        : base(context) { }
}

public class ApplicationRoleStore : RoleStore<ApplicationRole, ApplicationDbContext, int>
{
    public ApplicationRoleStore(ApplicationDbContext context)
        : base(context) { }
}
