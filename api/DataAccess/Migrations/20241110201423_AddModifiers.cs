using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PointOfSale.DataAccess.Shared.Migrations
{
    /// <inheritdoc />
    public partial class AddModifiers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTax_Products_ProductsId",
                schema: "Order",
                table: "ProductTax"
            );

            migrationBuilder.DropForeignKey(name: "FK_ProductTax_Taxes_TaxesId", schema: "Order", table: "ProductTax");

            migrationBuilder.DropPrimaryKey(name: "PK_ProductTax", schema: "Order", table: "ProductTax");

            migrationBuilder.RenameTable(
                name: "ProductTax",
                schema: "Order",
                newName: "ProductTaxes",
                newSchema: "Order"
            );

            migrationBuilder.RenameIndex(
                name: "IX_ProductTax_TaxesId",
                schema: "Order",
                table: "ProductTaxes",
                newName: "IX_ProductTaxes_TaxesId"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTaxes",
                schema: "Order",
                table: "ProductTaxes",
                columns: new[] { "ProductsId", "TaxesId" }
            );

            migrationBuilder.CreateTable(
                name: "Modifiers",
                schema: "Order",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modifiers", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "ProductModifiers",
                schema: "Order",
                columns: table => new
                {
                    ModifiersId = table.Column<int>(type: "integer", nullable: false),
                    ProductsId = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductModifiers", x => new { x.ModifiersId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ProductModifiers_Modifiers_ModifiersId",
                        column: x => x.ModifiersId,
                        principalSchema: "Order",
                        principalTable: "Modifiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_ProductModifiers_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalSchema: "Order",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_ProductModifiers_ProductsId",
                schema: "Order",
                table: "ProductModifiers",
                column: "ProductsId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTaxes_Products_ProductsId",
                schema: "Order",
                table: "ProductTaxes",
                column: "ProductsId",
                principalSchema: "Order",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTaxes_Taxes_TaxesId",
                schema: "Order",
                table: "ProductTaxes",
                column: "TaxesId",
                principalSchema: "Order",
                principalTable: "Taxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTaxes_Products_ProductsId",
                schema: "Order",
                table: "ProductTaxes"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTaxes_Taxes_TaxesId",
                schema: "Order",
                table: "ProductTaxes"
            );

            migrationBuilder.DropTable(name: "ProductModifiers", schema: "Order");

            migrationBuilder.DropTable(name: "Modifiers", schema: "Order");

            migrationBuilder.DropPrimaryKey(name: "PK_ProductTaxes", schema: "Order", table: "ProductTaxes");

            migrationBuilder.RenameTable(
                name: "ProductTaxes",
                schema: "Order",
                newName: "ProductTax",
                newSchema: "Order"
            );

            migrationBuilder.RenameIndex(
                name: "IX_ProductTaxes_TaxesId",
                schema: "Order",
                table: "ProductTax",
                newName: "IX_ProductTax_TaxesId"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTax",
                schema: "Order",
                table: "ProductTax",
                columns: new[] { "ProductsId", "TaxesId" }
            );

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTax_Products_ProductsId",
                schema: "Order",
                table: "ProductTax",
                column: "ProductsId",
                principalSchema: "Order",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTax_Taxes_TaxesId",
                schema: "Order",
                table: "ProductTax",
                column: "TaxesId",
                principalSchema: "Order",
                principalTable: "Taxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
