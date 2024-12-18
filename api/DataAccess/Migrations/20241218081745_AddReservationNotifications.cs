using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.DataAccess.Shared.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationNotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Customer_PhoneNumber",
                schema: "Order",
                table: "Reservations",
                type: "text",
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<Guid>(
                name: "Notification_IdempotencyKey",
                schema: "Order",
                table: "Reservations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000")
            );

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Notification_SentAt",
                schema: "Order",
                table: "Reservations",
                type: "timestamp with time zone",
                nullable: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Customer_PhoneNumber", schema: "Order", table: "Reservations");

            migrationBuilder.DropColumn(name: "Notification_IdempotencyKey", schema: "Order", table: "Reservations");

            migrationBuilder.DropColumn(name: "Notification_SentAt", schema: "Order", table: "Reservations");
        }
    }
}
