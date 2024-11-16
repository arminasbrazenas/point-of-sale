using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.DataAccess.Shared.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRowVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "RowVersion", schema: "Order", table: "Products");

            migrationBuilder.DropColumn(name: "RowVersion", schema: "Order", table: "Modifiers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "Order",
                table: "Products",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]
            );

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "Order",
                table: "Modifiers",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]
            );
        }
    }
}
