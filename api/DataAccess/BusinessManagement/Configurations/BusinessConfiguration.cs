using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Models.BusinessManagement.Entities;

namespace PointOfSale.DataAccess.BusinessManagement.Configurations;

public class BusinessConfiguration : EntityBaseConfiguration<Business, int>
{
    private const string TableName = "Businesses";

    public override void Configure(EntityTypeBuilder<Business> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(b => b.BusinessOwner)
            .WithOne(u => u.OwnedBusiness)
            .HasForeignKey<Business>(b => b.BusinessOwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(b => b.Employees)
            .WithOne(u => u.EmployerBusiness)
            .HasForeignKey(u => u.EmployerBusinessId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(b => b.WorkingHours);

        builder.ToTable(TableName, Constants.SchemaName);
    }
}
