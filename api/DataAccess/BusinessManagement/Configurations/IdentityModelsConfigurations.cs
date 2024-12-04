using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PointOfSale.DataAccess.BusinessManagement.Configurations;

public class IdentityUserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<int>> builder)
    {
        builder.ToTable("UserLogins");
        builder.HasKey(login => new { login.LoginProvider, login.ProviderKey });
    }
}

public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
    {
        builder.ToTable("UserRoles");

        builder.HasKey(role => new { role.UserId, role.RoleId });
    }
}

public class IdentityUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<int>> builder)
    {
        builder.ToTable("UserTokens");

        builder.HasKey(token => new
        {
            token.UserId,
            token.LoginProvider,
            token.Name,
        });
    }
}
