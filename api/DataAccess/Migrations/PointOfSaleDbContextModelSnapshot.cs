﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PointOfSale.DataAccess;

#nullable disable

namespace PointOfSale.DataAccess.Shared.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class PointOfSaleDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ModifierProduct", b =>
                {
                    b.Property<int>("ModifiersId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductsId")
                        .HasColumnType("integer");

                    b.HasKey("ModifiersId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("ProductModifiers", "Order");
                });

            modelBuilder.Entity("PointOfSale.Models.OrderManagement.Entities.Modifier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<decimal>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)");

                    b.Property<uint>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.Property<int>("Stock")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Modifiers", "Order");
                });

            modelBuilder.Entity("PointOfSale.Models.OrderManagement.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Orders", "Order");
                });

            modelBuilder.Entity("PointOfSale.Models.OrderManagement.Entities.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("BaseUnitPrice")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int?>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems", "Order");
                });

            modelBuilder.Entity("PointOfSale.Models.OrderManagement.Entities.OrderItemModifier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("ModifierId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("OrderItemId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("OrderItemId");

                    b.ToTable("OrderItemModifiers", "Order");
                });

            modelBuilder.Entity("PointOfSale.Models.OrderManagement.Entities.OrderItemTax", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("OrderItemId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Rate")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("OrderItemId");

                    b.ToTable("OrderItemTaxes", "Order");
                });

            modelBuilder.Entity("PointOfSale.Models.OrderManagement.Entities.OrderServiceCharge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<string>("PricingStrategy")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderServiceCharges", "Order");
                });

            modelBuilder.Entity("PointOfSale.Models.OrderManagement.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<decimal>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)");

                    b.Property<uint>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.Property<int>("Stock")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Products", "Order");
                });

            modelBuilder.Entity("PointOfSale.Models.OrderManagement.Entities.ServiceCharge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PricingStrategy")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("ServiceCharges", "Order");
                });

            modelBuilder.Entity("PointOfSale.Models.OrderManagement.Entities.Tax", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal>("Rate")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Taxes", "Order");
                });

            modelBuilder.Entity("PointOfSale.Models.ReservationManagement.Entities.ContactInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ContactInfo", "Reservation");
                });

            modelBuilder.Entity("PointOfSale.Models.ReservationManagement.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ContactInfoId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ServiceId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("ContactInfoId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Reservations", "Reservation");
                });

            modelBuilder.Entity("PointOfSale.Models.ReservationManagement.Entities.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AvailableFrom")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("AvailableTo")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.Property<DateTimeOffset>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Services", "Reservation");
                });

            modelBuilder.Entity("PointOfSale.Models.ReservationManagement.Entities.ServiceAvailability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Priority")
                        .HasMaxLength(100)
                        .HasColumnType("integer");

                    b.Property<int>("ServiceId")
                        .HasColumnType("integer");

                    b.Property<int>("ServiceResourceId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.HasIndex("ServiceResourceId");

                    b.ToTable("ServiceAvailability", "Reservation");
                });

            modelBuilder.Entity("PointOfSale.Models.ReservationManagement.Entities.ServiceResource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("ServiceResources", "Reservation");
                });

            modelBuilder.Entity("ProductTax", b =>
                {
                    b.Property<int>("ProductsId")
                        .HasColumnType("integer");

                    b.Property<int>("TaxesId")
                        .HasColumnType("integer");

                    b.HasKey("ProductsId", "TaxesId");

                    b.HasIndex("TaxesId");

                    b.ToTable("ProductTaxes", "Order");
                });

            modelBuilder.Entity("ModifierProduct", b =>
                {
                    b.HasOne("PointOfSale.Models.OrderManagement.Entities.Modifier", null)
                        .WithMany()
                        .HasForeignKey("ModifiersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PointOfSale.Models.OrderManagement.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PointOfSale.Models.OrderManagement.Entities.OrderItem", b =>
                {
                    b.HasOne("PointOfSale.Models.OrderManagement.Entities.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PointOfSale.Models.OrderManagement.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("PointOfSale.Models.OrderManagement.Entities.OrderItemModifier", b =>
                {
                    b.HasOne("PointOfSale.Models.OrderManagement.Entities.OrderItem", null)
                        .WithMany("Modifiers")
                        .HasForeignKey("OrderItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PointOfSale.Models.OrderManagement.Entities.OrderItemTax", b =>
                {
                    b.HasOne("PointOfSale.Models.OrderManagement.Entities.OrderItem", "OrderItem")
                        .WithMany("Taxes")
                        .HasForeignKey("OrderItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderItem");
                });

            modelBuilder.Entity("PointOfSale.Models.OrderManagement.Entities.OrderServiceCharge", b =>
                {
                    b.HasOne("PointOfSale.Models.OrderManagement.Entities.Order", null)
                        .WithMany("ServiceCharges")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PointOfSale.Models.ReservationManagement.Entities.Reservation", b =>
                {
                    b.HasOne("PointOfSale.Models.ReservationManagement.Entities.ContactInfo", "ContactInfo")
                        .WithMany("Reservations")
                        .HasForeignKey("ContactInfoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PointOfSale.Models.ReservationManagement.Entities.Service", "Service")
                        .WithMany("Reservations")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ContactInfo");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("PointOfSale.Models.ReservationManagement.Entities.ServiceAvailability", b =>
                {
                    b.HasOne("PointOfSale.Models.ReservationManagement.Entities.Service", "Service")
                        .WithMany("ServiceAvailability")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PointOfSale.Models.ReservationManagement.Entities.ServiceResource", "ServiceResource")
                        .WithMany("ServiceAvailability")
                        .HasForeignKey("ServiceResourceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Service");

                    b.Navigation("ServiceResource");
                });

            modelBuilder.Entity("ProductTax", b =>
                {
                    b.HasOne("PointOfSale.Models.OrderManagement.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PointOfSale.Models.OrderManagement.Entities.Tax", null)
                        .WithMany()
                        .HasForeignKey("TaxesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PointOfSale.Models.OrderManagement.Entities.Order", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("ServiceCharges");
                });

            modelBuilder.Entity("PointOfSale.Models.OrderManagement.Entities.OrderItem", b =>
                {
                    b.Navigation("Modifiers");

                    b.Navigation("Taxes");
                });

            modelBuilder.Entity("PointOfSale.Models.ReservationManagement.Entities.ContactInfo", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("PointOfSale.Models.ReservationManagement.Entities.Service", b =>
                {
                    b.Navigation("Reservations");

                    b.Navigation("ServiceAvailability");
                });

            modelBuilder.Entity("PointOfSale.Models.ReservationManagement.Entities.ServiceResource", b =>
                {
                    b.Navigation("ServiceAvailability");
                });
#pragma warning restore 612, 618
        }
    }
}
