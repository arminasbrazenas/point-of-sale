using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.DataAccess.Shared.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                schema: "Payment",
                table: "Tips",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                schema: "Payment",
                table: "Tips",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                schema: "Order",
                table: "Taxes",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                schema: "Order",
                table: "Taxes",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                schema: "Order",
                table: "ServiceCharges",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                schema: "Order",
                table: "ServiceCharges",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                schema: "ApplicationUsers",
                table: "RefreshTokens",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                schema: "ApplicationUsers",
                table: "RefreshTokens",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                schema: "Order",
                table: "Products",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                schema: "Order",
                table: "Products",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                schema: "Order",
                table: "OrderServiceCharges",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                schema: "Order",
                table: "OrderServiceCharges",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                schema: "Order",
                table: "Orders",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                schema: "Order",
                table: "Orders",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                schema: "Payment",
                table: "OrderPayments",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                schema: "Payment",
                table: "OrderPayments",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                schema: "Order",
                table: "OrderItemTaxes",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                schema: "Order",
                table: "OrderItemTaxes",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                schema: "Order",
                table: "OrderItems",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                schema: "Order",
                table: "OrderItems",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                schema: "Order",
                table: "OrderItemModifiers",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                schema: "Order",
                table: "OrderItemModifiers",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                schema: "Order",
                table: "OrderItemDiscounts",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                schema: "Order",
                table: "OrderItemDiscounts",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                schema: "Order",
                table: "Modifiers",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                schema: "Order",
                table: "Modifiers",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                schema: "Payment",
                table: "GiftCards",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                schema: "Payment",
                table: "GiftCards",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                schema: "Order",
                table: "Discounts",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                schema: "Order",
                table: "Discounts",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                schema: "Business",
                table: "Businesses",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                schema: "Business",
                table: "Businesses",
                type: "integer",
                nullable: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_Tips_CreatedById",
                schema: "Payment",
                table: "Tips",
                column: "CreatedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Tips_ModifiedById",
                schema: "Payment",
                table: "Tips",
                column: "ModifiedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Taxes_CreatedById",
                schema: "Order",
                table: "Taxes",
                column: "CreatedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Taxes_ModifiedById",
                schema: "Order",
                table: "Taxes",
                column: "ModifiedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCharges_CreatedById",
                schema: "Order",
                table: "ServiceCharges",
                column: "CreatedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCharges_ModifiedById",
                schema: "Order",
                table: "ServiceCharges",
                column: "ModifiedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_CreatedById",
                schema: "ApplicationUsers",
                table: "RefreshTokens",
                column: "CreatedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_ModifiedById",
                schema: "ApplicationUsers",
                table: "RefreshTokens",
                column: "ModifiedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedById",
                schema: "Order",
                table: "Products",
                column: "CreatedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Products_ModifiedById",
                schema: "Order",
                table: "Products",
                column: "ModifiedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderServiceCharges_CreatedById",
                schema: "Order",
                table: "OrderServiceCharges",
                column: "CreatedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderServiceCharges_ModifiedById",
                schema: "Order",
                table: "OrderServiceCharges",
                column: "ModifiedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatedById",
                schema: "Order",
                table: "Orders",
                column: "CreatedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ModifiedById",
                schema: "Order",
                table: "Orders",
                column: "ModifiedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderPayments_CreatedById",
                schema: "Payment",
                table: "OrderPayments",
                column: "CreatedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderPayments_ModifiedById",
                schema: "Payment",
                table: "OrderPayments",
                column: "ModifiedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemTaxes_CreatedById",
                schema: "Order",
                table: "OrderItemTaxes",
                column: "CreatedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemTaxes_ModifiedById",
                schema: "Order",
                table: "OrderItemTaxes",
                column: "ModifiedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_CreatedById",
                schema: "Order",
                table: "OrderItems",
                column: "CreatedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ModifiedById",
                schema: "Order",
                table: "OrderItems",
                column: "ModifiedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemModifiers_CreatedById",
                schema: "Order",
                table: "OrderItemModifiers",
                column: "CreatedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemModifiers_ModifiedById",
                schema: "Order",
                table: "OrderItemModifiers",
                column: "ModifiedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemDiscounts_CreatedById",
                schema: "Order",
                table: "OrderItemDiscounts",
                column: "CreatedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemDiscounts_ModifiedById",
                schema: "Order",
                table: "OrderItemDiscounts",
                column: "ModifiedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Modifiers_CreatedById",
                schema: "Order",
                table: "Modifiers",
                column: "CreatedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Modifiers_ModifiedById",
                schema: "Order",
                table: "Modifiers",
                column: "ModifiedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_GiftCards_CreatedById",
                schema: "Payment",
                table: "GiftCards",
                column: "CreatedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_GiftCards_ModifiedById",
                schema: "Payment",
                table: "GiftCards",
                column: "ModifiedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_CreatedById",
                schema: "Order",
                table: "Discounts",
                column: "CreatedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_ModifiedById",
                schema: "Order",
                table: "Discounts",
                column: "ModifiedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_CreatedById",
                schema: "Business",
                table: "Businesses",
                column: "CreatedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_ModifiedById",
                schema: "Business",
                table: "Businesses",
                column: "ModifiedById"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_Users_CreatedById",
                schema: "Business",
                table: "Businesses",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_Users_ModifiedById",
                schema: "Business",
                table: "Businesses",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Users_CreatedById",
                schema: "Order",
                table: "Discounts",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Users_ModifiedById",
                schema: "Order",
                table: "Discounts",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_GiftCards_Users_CreatedById",
                schema: "Payment",
                table: "GiftCards",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_GiftCards_Users_ModifiedById",
                schema: "Payment",
                table: "GiftCards",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Modifiers_Users_CreatedById",
                schema: "Order",
                table: "Modifiers",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Modifiers_Users_ModifiedById",
                schema: "Order",
                table: "Modifiers",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemDiscounts_Users_CreatedById",
                schema: "Order",
                table: "OrderItemDiscounts",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemDiscounts_Users_ModifiedById",
                schema: "Order",
                table: "OrderItemDiscounts",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemModifiers_Users_CreatedById",
                schema: "Order",
                table: "OrderItemModifiers",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemModifiers_Users_ModifiedById",
                schema: "Order",
                table: "OrderItemModifiers",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Users_CreatedById",
                schema: "Order",
                table: "OrderItems",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Users_ModifiedById",
                schema: "Order",
                table: "OrderItems",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemTaxes_Users_CreatedById",
                schema: "Order",
                table: "OrderItemTaxes",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemTaxes_Users_ModifiedById",
                schema: "Order",
                table: "OrderItemTaxes",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPayments_Users_CreatedById",
                schema: "Payment",
                table: "OrderPayments",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPayments_Users_ModifiedById",
                schema: "Payment",
                table: "OrderPayments",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_CreatedById",
                schema: "Order",
                table: "Orders",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_ModifiedById",
                schema: "Order",
                table: "Orders",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderServiceCharges_Users_CreatedById",
                schema: "Order",
                table: "OrderServiceCharges",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderServiceCharges_Users_ModifiedById",
                schema: "Order",
                table: "OrderServiceCharges",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_CreatedById",
                schema: "Order",
                table: "Products",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_ModifiedById",
                schema: "Order",
                table: "Products",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_CreatedById",
                schema: "ApplicationUsers",
                table: "RefreshTokens",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_ModifiedById",
                schema: "ApplicationUsers",
                table: "RefreshTokens",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCharges_Users_CreatedById",
                schema: "Order",
                table: "ServiceCharges",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCharges_Users_ModifiedById",
                schema: "Order",
                table: "ServiceCharges",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Taxes_Users_CreatedById",
                schema: "Order",
                table: "Taxes",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Taxes_Users_ModifiedById",
                schema: "Order",
                table: "Taxes",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Users_CreatedById",
                schema: "Payment",
                table: "Tips",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Users_ModifiedById",
                schema: "Payment",
                table: "Tips",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_Users_CreatedById",
                schema: "Business",
                table: "Businesses"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_Users_ModifiedById",
                schema: "Business",
                table: "Businesses"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Users_CreatedById",
                schema: "Order",
                table: "Discounts"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Users_ModifiedById",
                schema: "Order",
                table: "Discounts"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_GiftCards_Users_CreatedById",
                schema: "Payment",
                table: "GiftCards"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_GiftCards_Users_ModifiedById",
                schema: "Payment",
                table: "GiftCards"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_Modifiers_Users_CreatedById",
                schema: "Order",
                table: "Modifiers"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_Modifiers_Users_ModifiedById",
                schema: "Order",
                table: "Modifiers"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItemDiscounts_Users_CreatedById",
                schema: "Order",
                table: "OrderItemDiscounts"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItemDiscounts_Users_ModifiedById",
                schema: "Order",
                table: "OrderItemDiscounts"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItemModifiers_Users_CreatedById",
                schema: "Order",
                table: "OrderItemModifiers"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItemModifiers_Users_ModifiedById",
                schema: "Order",
                table: "OrderItemModifiers"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Users_CreatedById",
                schema: "Order",
                table: "OrderItems"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Users_ModifiedById",
                schema: "Order",
                table: "OrderItems"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItemTaxes_Users_CreatedById",
                schema: "Order",
                table: "OrderItemTaxes"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItemTaxes_Users_ModifiedById",
                schema: "Order",
                table: "OrderItemTaxes"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPayments_Users_CreatedById",
                schema: "Payment",
                table: "OrderPayments"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPayments_Users_ModifiedById",
                schema: "Payment",
                table: "OrderPayments"
            );

            migrationBuilder.DropForeignKey(name: "FK_Orders_Users_CreatedById", schema: "Order", table: "Orders");

            migrationBuilder.DropForeignKey(name: "FK_Orders_Users_ModifiedById", schema: "Order", table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderServiceCharges_Users_CreatedById",
                schema: "Order",
                table: "OrderServiceCharges"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_OrderServiceCharges_Users_ModifiedById",
                schema: "Order",
                table: "OrderServiceCharges"
            );

            migrationBuilder.DropForeignKey(name: "FK_Products_Users_CreatedById", schema: "Order", table: "Products");

            migrationBuilder.DropForeignKey(name: "FK_Products_Users_ModifiedById", schema: "Order", table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_CreatedById",
                schema: "ApplicationUsers",
                table: "RefreshTokens"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_ModifiedById",
                schema: "ApplicationUsers",
                table: "RefreshTokens"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCharges_Users_CreatedById",
                schema: "Order",
                table: "ServiceCharges"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCharges_Users_ModifiedById",
                schema: "Order",
                table: "ServiceCharges"
            );

            migrationBuilder.DropForeignKey(name: "FK_Taxes_Users_CreatedById", schema: "Order", table: "Taxes");

            migrationBuilder.DropForeignKey(name: "FK_Taxes_Users_ModifiedById", schema: "Order", table: "Taxes");

            migrationBuilder.DropForeignKey(name: "FK_Tips_Users_CreatedById", schema: "Payment", table: "Tips");

            migrationBuilder.DropForeignKey(name: "FK_Tips_Users_ModifiedById", schema: "Payment", table: "Tips");

            migrationBuilder.DropIndex(name: "IX_Tips_CreatedById", schema: "Payment", table: "Tips");

            migrationBuilder.DropIndex(name: "IX_Tips_ModifiedById", schema: "Payment", table: "Tips");

            migrationBuilder.DropIndex(name: "IX_Taxes_CreatedById", schema: "Order", table: "Taxes");

            migrationBuilder.DropIndex(name: "IX_Taxes_ModifiedById", schema: "Order", table: "Taxes");

            migrationBuilder.DropIndex(name: "IX_ServiceCharges_CreatedById", schema: "Order", table: "ServiceCharges");

            migrationBuilder.DropIndex(
                name: "IX_ServiceCharges_ModifiedById",
                schema: "Order",
                table: "ServiceCharges"
            );

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_CreatedById",
                schema: "ApplicationUsers",
                table: "RefreshTokens"
            );

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_ModifiedById",
                schema: "ApplicationUsers",
                table: "RefreshTokens"
            );

            migrationBuilder.DropIndex(name: "IX_Products_CreatedById", schema: "Order", table: "Products");

            migrationBuilder.DropIndex(name: "IX_Products_ModifiedById", schema: "Order", table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_OrderServiceCharges_CreatedById",
                schema: "Order",
                table: "OrderServiceCharges"
            );

            migrationBuilder.DropIndex(
                name: "IX_OrderServiceCharges_ModifiedById",
                schema: "Order",
                table: "OrderServiceCharges"
            );

            migrationBuilder.DropIndex(name: "IX_Orders_CreatedById", schema: "Order", table: "Orders");

            migrationBuilder.DropIndex(name: "IX_Orders_ModifiedById", schema: "Order", table: "Orders");

            migrationBuilder.DropIndex(name: "IX_OrderPayments_CreatedById", schema: "Payment", table: "OrderPayments");

            migrationBuilder.DropIndex(
                name: "IX_OrderPayments_ModifiedById",
                schema: "Payment",
                table: "OrderPayments"
            );

            migrationBuilder.DropIndex(name: "IX_OrderItemTaxes_CreatedById", schema: "Order", table: "OrderItemTaxes");

            migrationBuilder.DropIndex(
                name: "IX_OrderItemTaxes_ModifiedById",
                schema: "Order",
                table: "OrderItemTaxes"
            );

            migrationBuilder.DropIndex(name: "IX_OrderItems_CreatedById", schema: "Order", table: "OrderItems");

            migrationBuilder.DropIndex(name: "IX_OrderItems_ModifiedById", schema: "Order", table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItemModifiers_CreatedById",
                schema: "Order",
                table: "OrderItemModifiers"
            );

            migrationBuilder.DropIndex(
                name: "IX_OrderItemModifiers_ModifiedById",
                schema: "Order",
                table: "OrderItemModifiers"
            );

            migrationBuilder.DropIndex(
                name: "IX_OrderItemDiscounts_CreatedById",
                schema: "Order",
                table: "OrderItemDiscounts"
            );

            migrationBuilder.DropIndex(
                name: "IX_OrderItemDiscounts_ModifiedById",
                schema: "Order",
                table: "OrderItemDiscounts"
            );

            migrationBuilder.DropIndex(name: "IX_Modifiers_CreatedById", schema: "Order", table: "Modifiers");

            migrationBuilder.DropIndex(name: "IX_Modifiers_ModifiedById", schema: "Order", table: "Modifiers");

            migrationBuilder.DropIndex(name: "IX_GiftCards_CreatedById", schema: "Payment", table: "GiftCards");

            migrationBuilder.DropIndex(name: "IX_GiftCards_ModifiedById", schema: "Payment", table: "GiftCards");

            migrationBuilder.DropIndex(name: "IX_Discounts_CreatedById", schema: "Order", table: "Discounts");

            migrationBuilder.DropIndex(name: "IX_Discounts_ModifiedById", schema: "Order", table: "Discounts");

            migrationBuilder.DropIndex(name: "IX_Businesses_CreatedById", schema: "Business", table: "Businesses");

            migrationBuilder.DropIndex(name: "IX_Businesses_ModifiedById", schema: "Business", table: "Businesses");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Payment", table: "Tips");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Payment", table: "Tips");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Order", table: "Taxes");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Order", table: "Taxes");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Order", table: "ServiceCharges");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Order", table: "ServiceCharges");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "ApplicationUsers", table: "RefreshTokens");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "ApplicationUsers", table: "RefreshTokens");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Order", table: "Products");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Order", table: "Products");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Order", table: "OrderServiceCharges");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Order", table: "OrderServiceCharges");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Order", table: "Orders");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Order", table: "Orders");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Payment", table: "OrderPayments");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Payment", table: "OrderPayments");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Order", table: "OrderItemTaxes");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Order", table: "OrderItemTaxes");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Order", table: "OrderItems");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Order", table: "OrderItems");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Order", table: "OrderItemModifiers");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Order", table: "OrderItemModifiers");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Order", table: "OrderItemDiscounts");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Order", table: "OrderItemDiscounts");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Order", table: "Modifiers");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Order", table: "Modifiers");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Payment", table: "GiftCards");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Payment", table: "GiftCards");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Order", table: "Discounts");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Order", table: "Discounts");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Business", table: "Businesses");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Business", table: "Businesses");
        }
    }
}
