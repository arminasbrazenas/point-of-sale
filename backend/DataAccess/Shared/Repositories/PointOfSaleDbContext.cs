using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.Order.Entities;
using PointOfSale.DataAccess.Shared.Interceptors;

namespace PointOfSale.DataAccess.Shared.Repositories;

public class PointOfSaleDbContext : DbContext
{
    private static readonly AuditingInterceptor AuditingInterceptor = new();

    public DbSet<Tax> Taxes { get; set; }

    public PointOfSaleDbContext(DbContextOptions<PointOfSaleDbContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(AuditingInterceptor);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
