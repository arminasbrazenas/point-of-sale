using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.DataAccess.Shared.Migrations
{
    /// <inheritdoc />
    public partial class Discountsv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "GrossPrice", schema: "Order", table: "OrderItemModifiers");

            migrationBuilder.RenameColumn(
                name: "BaseUnitGrossPrice",
                schema: "Order",
                table: "OrderItems",
                newName: "BaseUnitPrice"
            );

            migrationBuilder.RenameColumn(
                name: "AppliedUnitAmount",
                schema: "Order",
                table: "OrderItemTaxes",
                newName: "AppliedAmount"
            );

            migrationBuilder.RenameColumn(
                name: "TaxTotal",
                schema: "Order",
                table: "OrderItemModifiers",
                newName: "Price"
            );

            migrationBuilder.RenameColumn(
                name: "AppliedUnitAmount",
                schema: "Order",
                table: "OrderItemDiscounts",
                newName: "AppliedAmount"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BaseUnitPrice",
                schema: "Order",
                table: "OrderItems",
                newName: "BaseUnitGrossPrice"
            );

            migrationBuilder.RenameColumn(
                name: "AppliedAmount",
                schema: "Order",
                table: "OrderItemTaxes",
                newName: "AppliedUnitAmount"
            );

            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "Order",
                table: "OrderItemModifiers",
                newName: "TaxTotal"
            );

            migrationBuilder.RenameColumn(
                name: "AppliedAmount",
                schema: "Order",
                table: "OrderItemDiscounts",
                newName: "AppliedUnitAmount"
            );

            migrationBuilder.AddColumn<decimal>(
                name: "GrossPrice",
                schema: "Order",
                table: "OrderItemModifiers",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m
            );
        }
    }
}
