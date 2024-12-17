using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.DataAccess.Shared.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePaymentsAuthorization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiftCards_Users_CreatedById",
                schema: "Payment",
                table: "GiftCards");

            migrationBuilder.DropForeignKey(
                name: "FK_GiftCards_Users_ModifiedById",
                schema: "Payment",
                table: "GiftCards");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPayments_Users_CreatedById",
                schema: "Payment",
                table: "OrderPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPayments_Users_ModifiedById",
                schema: "Payment",
                table: "OrderPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Tips_Users_CreatedById",
                schema: "Payment",
                table: "Tips");

            migrationBuilder.DropForeignKey(
                name: "FK_Tips_Users_ModifiedById",
                schema: "Payment",
                table: "Tips");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                schema: "Payment",
                table: "Tips",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BusinessId",
                schema: "Payment",
                table: "OrderPayments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                schema: "Payment",
                table: "OrderPayments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BusinessId",
                schema: "Payment",
                table: "GiftCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tips_EmployeeId",
                schema: "Payment",
                table: "Tips",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPayments_BusinessId",
                schema: "Payment",
                table: "OrderPayments",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPayments_EmployeeId",
                schema: "Payment",
                table: "OrderPayments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftCards_BusinessId",
                schema: "Payment",
                table: "GiftCards",
                column: "BusinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_GiftCards_Businesses_BusinessId",
                schema: "Payment",
                table: "GiftCards",
                column: "BusinessId",
                principalSchema: "Business",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GiftCards_Users_CreatedById",
                schema: "Payment",
                table: "GiftCards",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_GiftCards_Users_ModifiedById",
                schema: "Payment",
                table: "GiftCards",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPayments_Businesses_BusinessId",
                schema: "Payment",
                table: "OrderPayments",
                column: "BusinessId",
                principalSchema: "Business",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPayments_Users_CreatedById",
                schema: "Payment",
                table: "OrderPayments",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPayments_Users_EmployeeId",
                schema: "Payment",
                table: "OrderPayments",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPayments_Users_ModifiedById",
                schema: "Payment",
                table: "OrderPayments",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Users_CreatedById",
                schema: "Payment",
                table: "Tips",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Users_EmployeeId",
                schema: "Payment",
                table: "Tips",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Users_ModifiedById",
                schema: "Payment",
                table: "Tips",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiftCards_Businesses_BusinessId",
                schema: "Payment",
                table: "GiftCards");

            migrationBuilder.DropForeignKey(
                name: "FK_GiftCards_Users_CreatedById",
                schema: "Payment",
                table: "GiftCards");

            migrationBuilder.DropForeignKey(
                name: "FK_GiftCards_Users_ModifiedById",
                schema: "Payment",
                table: "GiftCards");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPayments_Businesses_BusinessId",
                schema: "Payment",
                table: "OrderPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPayments_Users_CreatedById",
                schema: "Payment",
                table: "OrderPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPayments_Users_EmployeeId",
                schema: "Payment",
                table: "OrderPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPayments_Users_ModifiedById",
                schema: "Payment",
                table: "OrderPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Tips_Users_CreatedById",
                schema: "Payment",
                table: "Tips");

            migrationBuilder.DropForeignKey(
                name: "FK_Tips_Users_EmployeeId",
                schema: "Payment",
                table: "Tips");

            migrationBuilder.DropForeignKey(
                name: "FK_Tips_Users_ModifiedById",
                schema: "Payment",
                table: "Tips");

            migrationBuilder.DropIndex(
                name: "IX_Tips_EmployeeId",
                schema: "Payment",
                table: "Tips");

            migrationBuilder.DropIndex(
                name: "IX_OrderPayments_BusinessId",
                schema: "Payment",
                table: "OrderPayments");

            migrationBuilder.DropIndex(
                name: "IX_OrderPayments_EmployeeId",
                schema: "Payment",
                table: "OrderPayments");

            migrationBuilder.DropIndex(
                name: "IX_GiftCards_BusinessId",
                schema: "Payment",
                table: "GiftCards");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "Payment",
                table: "Tips");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                schema: "Payment",
                table: "OrderPayments");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "Payment",
                table: "OrderPayments");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                schema: "Payment",
                table: "GiftCards");

            migrationBuilder.AddForeignKey(
                name: "FK_GiftCards_Users_CreatedById",
                schema: "Payment",
                table: "GiftCards",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GiftCards_Users_ModifiedById",
                schema: "Payment",
                table: "GiftCards",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPayments_Users_CreatedById",
                schema: "Payment",
                table: "OrderPayments",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPayments_Users_ModifiedById",
                schema: "Payment",
                table: "OrderPayments",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Users_CreatedById",
                schema: "Payment",
                table: "Tips",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Users_ModifiedById",
                schema: "Payment",
                table: "Tips",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
