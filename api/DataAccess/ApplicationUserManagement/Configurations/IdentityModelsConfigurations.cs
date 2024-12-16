using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Models.ApplicationUserManagement.Entities;
using PointOfSale.Models.BusinessManagement.Entities;

namespace PointOfSale.DataAccess.BusinessManagement.Configurations;

public class IdentityUserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<int>>
{
    private const string TableName = "UserLogins";

    public void Configure(EntityTypeBuilder<IdentityUserLogin<int>> builder)
    {
        builder.HasKey(login => new { login.LoginProvider, login.ProviderKey });
        builder.ToTable(TableName);
    }
}

public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<int>>
{
    private const string TableName = "UserRoles";

    public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
    {
        builder.HasKey(role => new { role.UserId, role.RoleId });
        builder.ToTable(TableName);
    }
}

public class IdentityUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<int>>
{
    private const string TableName = "UserTokens";

    public void Configure(EntityTypeBuilder<IdentityUserToken<int>> builder)
    {
        builder.HasKey(token => new
        {
            token.UserId,
            token.LoginProvider,
            token.Name,
        });
        builder.ToTable(TableName);
    }
}

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    private const string TableName = "Users";

    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .HasOne(u => u.OwnedBusiness)
            .WithOne(b => b.BusinessOwner)
            .HasForeignKey<Business>(b => b.BusinessOwnerId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.ToTable(TableName);
    }
}
