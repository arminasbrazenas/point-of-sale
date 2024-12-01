using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PointOfSale.DataAccess.BusinessManagement.Configurations;
public class IdentityUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<int>> builder)
    {
        builder.HasKey(userToken => new { userToken.UserId, userToken.LoginProvider, userToken.Name });
    }
}