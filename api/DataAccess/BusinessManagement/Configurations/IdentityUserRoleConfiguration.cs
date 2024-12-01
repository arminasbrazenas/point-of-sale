using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PointOfSale.DataAccess.BusinessManagement.Configurations;
public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
    {
        builder.HasKey(userRole => new { userRole.UserId, userRole.RoleId });
    }
}