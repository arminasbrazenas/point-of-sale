using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.DataAccess.Shared.Migrations
{
    /// <inheritdoc />
    public partial class ChangingCosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BaseUnitPrice",
                schema: "Order",
                table: "OrderItems",
                newName: "BaseUnitGrossPrice"
            );

            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "Order",
                table: "OrderItemModifiers",
                newName: "TaxTotal"
            );

            migrationBuilder.AddColumn<decimal>(
                name: "AppliedAmount",
                schema: "Order",
                table: "OrderItemTaxes",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m
            );

            migrationBuilder.AddColumn<decimal>(
                name: "AppliedUnitAmount",
                schema: "Order",
                table: "OrderItemTaxes",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m
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

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "Order",
                table: "OrderItemDiscounts",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)",
                oldPrecision: 10,
                oldScale: 2
            );

            migrationBuilder.AddColumn<decimal>(
                name: "AppliedAmount",
                schema: "Order",
                table: "OrderItemDiscounts",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m
            );

            migrationBuilder.AddColumn<decimal>(
                name: "AppliedUnitAmount",
                schema: "Order",
                table: "OrderItemDiscounts",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "AppliedAmount", schema: "Order", table: "OrderItemTaxes");

            migrationBuilder.DropColumn(name: "AppliedUnitAmount", schema: "Order", table: "OrderItemTaxes");

            migrationBuilder.DropColumn(name: "GrossPrice", schema: "Order", table: "OrderItemModifiers");

            migrationBuilder.DropColumn(name: "AppliedAmount", schema: "Order", table: "OrderItemDiscounts");

            migrationBuilder.DropColumn(name: "AppliedUnitAmount", schema: "Order", table: "OrderItemDiscounts");

            migrationBuilder.RenameColumn(
                name: "BaseUnitGrossPrice",
                schema: "Order",
                table: "OrderItems",
                newName: "BaseUnitPrice"
            );

            migrationBuilder.RenameColumn(
                name: "TaxTotal",
                schema: "Order",
                table: "OrderItemModifiers",
                newName: "Price"
            );

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "Order",
                table: "OrderItemDiscounts",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric"
            );
        }
    }
}
