using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PointOfSale.DataAccess.Shared.Migrations
{
    /// <inheritdoc />
    public partial class FixOrderItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                schema: "Order",
                table: "OrderItems"
            );

            migrationBuilder.RenameColumn(name: "TaxRate", schema: "Order", table: "OrderItemTaxes", newName: "Rate");

            migrationBuilder.RenameColumn(name: "TaxName", schema: "Order", table: "OrderItemTaxes", newName: "Name");

            migrationBuilder.RenameColumn(name: "Amount", schema: "Order", table: "Modifiers", newName: "Stock");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                schema: "Order",
                table: "OrderItems",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer"
            );

            migrationBuilder.CreateTable(
                name: "OrderItemModifiers",
                schema: "Order",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    OrderItemId = table.Column<int>(type: "integer", nullable: false),
                    ModifierId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemModifiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemModifiers_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalSchema: "Order",
                        principalTable: "OrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemModifiers_OrderItemId",
                schema: "Order",
                table: "OrderItemModifiers",
                column: "OrderItemId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                schema: "Order",
                table: "OrderItems",
                column: "ProductId",
                principalSchema: "Order",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                schema: "Order",
                table: "OrderItems"
            );

            migrationBuilder.DropTable(name: "OrderItemModifiers", schema: "Order");

            migrationBuilder.RenameColumn(name: "Rate", schema: "Order", table: "OrderItemTaxes", newName: "TaxRate");

            migrationBuilder.RenameColumn(name: "Name", schema: "Order", table: "OrderItemTaxes", newName: "TaxName");

            migrationBuilder.RenameColumn(name: "Stock", schema: "Order", table: "Modifiers", newName: "Amount");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                schema: "Order",
                table: "OrderItems",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                schema: "Order",
                table: "OrderItems",
                column: "ProductId",
                principalSchema: "Order",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
