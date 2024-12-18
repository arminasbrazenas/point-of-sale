using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.DataAccess.Shared.Migrations
{
    /// <inheritdoc />
    public partial class AddBusinessHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "WorkingHours_End",
                schema: "Business",
                table: "Businesses",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0)
            );

            migrationBuilder.AddColumn<TimeOnly>(
                name: "WorkingHours_Start",
                schema: "Business",
                table: "Businesses",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0)
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "WorkingHours_End", schema: "Business", table: "Businesses");

            migrationBuilder.DropColumn(name: "WorkingHours_Start", schema: "Business", table: "Businesses");
        }
    }
}
