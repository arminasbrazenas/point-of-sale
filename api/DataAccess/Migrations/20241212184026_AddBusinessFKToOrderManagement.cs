using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.DataAccess.Shared.Migrations
{
    /// <inheritdoc />
    public partial class AddBusinessFKToOrderManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessId",
                schema: "Order",
                table: "Taxes",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<int>(
                name: "BusinessId",
                schema: "Order",
                table: "ServiceCharges",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<int>(
                name: "BusinessId",
                schema: "Order",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<int>(
                name: "BusinessId",
                schema: "Order",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<int>(
                name: "BusinessId",
                schema: "Order",
                table: "Modifiers",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<int>(
                name: "BusinessId",
                schema: "Order",
                table: "Discounts",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.CreateIndex(
                name: "IX_Taxes_BusinessId",
                schema: "Order",
                table: "Taxes",
                column: "BusinessId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCharges_BusinessId",
                schema: "Order",
                table: "ServiceCharges",
                column: "BusinessId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Products_BusinessId",
                schema: "Order",
                table: "Products",
                column: "BusinessId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BusinessId",
                schema: "Order",
                table: "Orders",
                column: "BusinessId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Modifiers_BusinessId",
                schema: "Order",
                table: "Modifiers",
                column: "BusinessId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_BusinessId",
                schema: "Order",
                table: "Discounts",
                column: "BusinessId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Businesses_BusinessId",
                schema: "Order",
                table: "Discounts",
                column: "BusinessId",
                principalSchema: "Business",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Modifiers_Businesses_BusinessId",
                schema: "Order",
                table: "Modifiers",
                column: "BusinessId",
                principalSchema: "Business",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Businesses_BusinessId",
                schema: "Order",
                table: "Orders",
                column: "BusinessId",
                principalSchema: "Business",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Businesses_BusinessId",
                schema: "Order",
                table: "Products",
                column: "BusinessId",
                principalSchema: "Business",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCharges_Businesses_BusinessId",
                schema: "Order",
                table: "ServiceCharges",
                column: "BusinessId",
                principalSchema: "Business",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Taxes_Businesses_BusinessId",
                schema: "Order",
                table: "Taxes",
                column: "BusinessId",
                principalSchema: "Business",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Businesses_BusinessId",
                schema: "Order",
                table: "Discounts"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_Modifiers_Businesses_BusinessId",
                schema: "Order",
                table: "Modifiers"
            );

            migrationBuilder.DropForeignKey(name: "FK_Orders_Businesses_BusinessId", schema: "Order", table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Businesses_BusinessId",
                schema: "Order",
                table: "Products"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCharges_Businesses_BusinessId",
                schema: "Order",
                table: "ServiceCharges"
            );

            migrationBuilder.DropForeignKey(name: "FK_Taxes_Businesses_BusinessId", schema: "Order", table: "Taxes");

            migrationBuilder.DropIndex(name: "IX_Taxes_BusinessId", schema: "Order", table: "Taxes");

            migrationBuilder.DropIndex(name: "IX_ServiceCharges_BusinessId", schema: "Order", table: "ServiceCharges");

            migrationBuilder.DropIndex(name: "IX_Products_BusinessId", schema: "Order", table: "Products");

            migrationBuilder.DropIndex(name: "IX_Orders_BusinessId", schema: "Order", table: "Orders");

            migrationBuilder.DropIndex(name: "IX_Modifiers_BusinessId", schema: "Order", table: "Modifiers");

            migrationBuilder.DropIndex(name: "IX_Discounts_BusinessId", schema: "Order", table: "Discounts");

            migrationBuilder.DropColumn(name: "BusinessId", schema: "Order", table: "Taxes");

            migrationBuilder.DropColumn(name: "BusinessId", schema: "Order", table: "ServiceCharges");

            migrationBuilder.DropColumn(name: "BusinessId", schema: "Order", table: "Products");

            migrationBuilder.DropColumn(name: "BusinessId", schema: "Order", table: "Orders");

            migrationBuilder.DropColumn(name: "BusinessId", schema: "Order", table: "Modifiers");

            migrationBuilder.DropColumn(name: "BusinessId", schema: "Order", table: "Discounts");
        }
    }
}
