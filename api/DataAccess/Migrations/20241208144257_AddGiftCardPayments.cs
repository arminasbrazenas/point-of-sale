using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.DataAccess.Shared.Migrations
{
    /// <inheritdoc />
    public partial class AddGiftCardPayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GiftCardCode",
                schema: "Payment",
                table: "OrderPayments",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "GiftCardCode", schema: "Payment", table: "OrderPayments");
        }
    }
}
