using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PointOfSale.DataAccess.Shared.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderTaxes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Order",
                table: "OrderItems",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.CreateTable(
                name: "OrderItemTaxes",
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
                    TaxName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TaxRate = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemTaxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemTaxes_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalSchema: "Order",
                        principalTable: "OrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemTaxes_OrderItemId",
                schema: "Order",
                table: "OrderItemTaxes",
                column: "OrderItemId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "OrderItemTaxes", schema: "Order");

            migrationBuilder.DropColumn(name: "Name", schema: "Order", table: "OrderItems");
        }
    }
}
