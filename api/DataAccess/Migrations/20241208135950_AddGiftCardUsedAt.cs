using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.DataAccess.Shared.Migrations
{
    /// <inheritdoc />
    public partial class AddGiftCardUsedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Payment",
                table: "GiftCards",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text"
            );

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UsedAt",
                schema: "Payment",
                table: "GiftCards",
                type: "timestamp with time zone",
                nullable: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_GiftCards_Code",
                schema: "Payment",
                table: "GiftCards",
                column: "Code",
                unique: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name: "IX_GiftCards_Code", schema: "Payment", table: "GiftCards");

            migrationBuilder.DropColumn(name: "UsedAt", schema: "Payment", table: "GiftCards");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Payment",
                table: "GiftCards",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50
            );
        }
    }
}
