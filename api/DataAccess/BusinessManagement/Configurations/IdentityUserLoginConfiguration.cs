using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PointOfSale.DataAccess.BusinessManagement.Configurations;

public class IdentityUserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<int>> builder)
    {
        builder.HasKey(login => new { login.LoginProvider, login.ProviderKey });
    }
}