using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.DataAccess.Shared.Migrations
{
    /// <inheritdoc />
    public partial class ReReaddRowVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                schema: "Order",
                table: "Products",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u
            );

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                schema: "Order",
                table: "Modifiers",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "xmin", schema: "Order", table: "Products");

            migrationBuilder.DropColumn(name: "xmin", schema: "Order", table: "Modifiers");
        }
    }
}
