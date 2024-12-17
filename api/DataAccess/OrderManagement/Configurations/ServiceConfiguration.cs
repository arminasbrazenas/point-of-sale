using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.DataAccess.Shared;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Configurations;

public class ServiceConfiguration : EntityBaseConfiguration<Service, int>
{
    public override void Configure(EntityTypeBuilder<Service> builder)
    {
        base.Configure(builder);

        builder.Property(s => s.Name).HasMaxLength(Constants.ServiceNameMaxLength).IsRequired();

        builder.Property(s => s.Duration).IsRequired();

        builder
            .Property(s => s.Price)
            .HasPrecision(SharedConstants.MoneyPrecision, SharedConstants.MoneyScale)
            .IsRequired();

        builder.HasOne(s => s.Business).WithMany().HasForeignKey(s => s.BusinessId).IsRequired();

        builder.HasMany(s => s.ProvidedByEmployees).WithMany();

        builder.ToTable("Services", Constants.SchemaName);
    }
}
