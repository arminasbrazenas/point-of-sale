using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PointOfSale.DataAccess.BusinessManagement.Configurations;
public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole<int>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<int>> builder)
    {
        builder.ToTable("Roles");
        builder.HasKey(role => role.Id);

        builder.Property(role => role.Name)
               .IsRequired()
               .HasMaxLength(256);

        builder.HasIndex(role => role.NormalizedName)
               .IsUnique()
               .HasDatabaseName("RoleNameIndex");
    }
}