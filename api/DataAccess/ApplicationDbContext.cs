using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.ApplicationUserManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interceptors;
using PointOfSale.Models.ApplicationUserManagement.Entities;
using PointOfSale.Models.BusinessManagement.Entities;
using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.PaymentManagement.Entities;
using PointOfSale.Models.PaymentManagement.Enums;

namespace PointOfSale.DataAccess;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    private readonly ICurrentApplicationUserAccessor _currentApplicationUserAccessor;

    public DbSet<Tax> Taxes { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Modifier> Modifiers { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OrderItemModifier> OrderItemModifiers { get; set; }
    public DbSet<OrderItemTax> OrderItemTaxes { get; set; }
    public DbSet<ServiceCharge> ServiceCharges { get; set; }
    public DbSet<Business> Businesses { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<OrderItemDiscount> OrderItemDiscounts { get; set; }
    public DbSet<OrderServiceCharge> OrderServiceCharges { get; set; }
    public DbSet<OrderDiscount> OrderDiscounts { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<GiftCard> GiftCards { get; set; }
    public DbSet<Tip> Tips { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentApplicationUserAccessor currentApplicationUserAccessor
    )
        : base(options)
    {
        _currentApplicationUserAccessor = currentApplicationUserAccessor;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new AuditingInterceptor(_currentApplicationUserAccessor));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
