using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.DataAccess.Shared.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuditFieldsOnDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_Users_CreatedById",
                schema: "Business",
                table: "Businesses",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_Users_ModifiedById",
                schema: "Business",
                table: "Businesses",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Users_CreatedById",
                schema: "Order",
                table: "Discounts",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Users_ModifiedById",
                schema: "Order",
                table: "Discounts",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Modifiers_Users_CreatedById",
                schema: "Order",
                table: "Modifiers",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Modifiers_Users_ModifiedById",
                schema: "Order",
                table: "Modifiers",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemDiscounts_Users_CreatedById",
                schema: "Order",
                table: "OrderItemDiscounts",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemDiscounts_Users_ModifiedById",
                schema: "Order",
                table: "OrderItemDiscounts",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemModifiers_Users_CreatedById",
                schema: "Order",
                table: "OrderItemModifiers",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemModifiers_Users_ModifiedById",
                schema: "Order",
                table: "OrderItemModifiers",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Users_CreatedById",
                schema: "Order",
                table: "OrderItems",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Users_ModifiedById",
                schema: "Order",
                table: "OrderItems",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemTaxes_Users_CreatedById",
                schema: "Order",
                table: "OrderItemTaxes",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemTaxes_Users_ModifiedById",
                schema: "Order",
                table: "OrderItemTaxes",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_CreatedById",
                schema: "Order",
                table: "Orders",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_ModifiedById",
                schema: "Order",
                table: "Orders",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderServiceCharges_Users_CreatedById",
                schema: "Order",
                table: "OrderServiceCharges",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderServiceCharges_Users_ModifiedById",
                schema: "Order",
                table: "OrderServiceCharges",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_CreatedById",
                schema: "Order",
                table: "Products",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_ModifiedById",
                schema: "Order",
                table: "Products",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCharges_Users_CreatedById",
                schema: "Order",
                table: "ServiceCharges",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCharges_Users_ModifiedById",
                schema: "Order",
                table: "ServiceCharges",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Taxes_Users_CreatedById",
                schema: "Order",
                table: "Taxes",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Taxes_Users_ModifiedById",
                schema: "Order",
                table: "Taxes",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
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
        }
    }
}
