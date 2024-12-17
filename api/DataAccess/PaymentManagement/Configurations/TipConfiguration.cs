using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.DataAccess.Shared;
using PointOfSale.Models.PaymentManagement.Entities;

namespace PointOfSale.DataAccess.PaymentManagement.Configurations;

public class TipConfiguration : EntityBaseConfiguration<Tip, int>
{
    public override void Configure(EntityTypeBuilder<Tip> builder)
    {
        base.Configure(builder);

        builder.HasKey(t => t.Id);

        builder
            .Property(t => t.Amount)
            .HasPrecision(SharedConstants.MoneyPrecision, SharedConstants.MoneyScale)
            .IsRequired();

        builder.HasOne(t => t.Order).WithMany().HasForeignKey(t => t.OrderId).IsRequired();

        builder.HasOne(o => o.Employee).WithMany().HasForeignKey(o => o.EmployeeId).IsRequired();

        builder.ToTable("Tips", Constants.SchemaName);
    }
}
