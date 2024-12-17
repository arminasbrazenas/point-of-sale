using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.DataAccess.Shared.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceEmployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUserService",
                columns: table => new
                {
                    ProvidedByEmployeesId = table.Column<int>(type: "integer", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserService", x => new { x.ProvidedByEmployeesId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserService_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "Order",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_ApplicationUserService_Users_ProvidedByEmployeesId",
                        column: x => x.ProvidedByEmployeesId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserService_ServiceId",
                table: "ApplicationUserService",
                column: "ServiceId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ApplicationUserService");
        }
    }
}
