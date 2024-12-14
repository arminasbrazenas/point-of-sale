using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Models.ApplicationUserManagement.Entities;

namespace PointOfSale.DataAccess.ApplicationUserManagement.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    private const string TableName = "RefreshTokens";

    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasOne(b => b.ApplicationUser).WithMany().HasForeignKey(b => b.ApplicationUserId).IsRequired();

        builder.ToTable(TableName, Constants.SchemaName);
    }
}
