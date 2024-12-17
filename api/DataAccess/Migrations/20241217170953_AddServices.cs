using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PointOfSale.DataAccess.Shared.Migrations
{
    /// <inheritdoc />
    public partial class AddServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                schema: "Order",
                table: "Orders",
                type: "integer",
                nullable: true
            );

            migrationBuilder.CreateTable(
                name: "Services",
                schema: "Order",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    BusinessId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalSchema: "Business",
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Services_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                        name: "FK_Services_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Reservations",
                schema: "Order",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    ServiceId = table.Column<int>(type: "integer", nullable: true),
                    Date_Start = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Date_End = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Customer_FirstName = table.Column<string>(
                        type: "character varying(50)",
                        maxLength: 50,
                        nullable: false
                    ),
                    Customer_LastName = table.Column<string>(
                        type: "character varying(50)",
                        maxLength: 50,
                        nullable: false
                    ),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    BusinessId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalSchema: "Business",
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Reservations_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "Order",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                        name: "FK_Reservations_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                        name: "FK_Reservations_Users_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Reservations_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ReservationId",
                schema: "Order",
                table: "Orders",
                column: "ReservationId",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_BusinessId",
                schema: "Order",
                table: "Reservations",
                column: "BusinessId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CreatedById",
                schema: "Order",
                table: "Reservations",
                column: "CreatedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_EmployeeId",
                schema: "Order",
                table: "Reservations",
                column: "EmployeeId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ModifiedById",
                schema: "Order",
                table: "Reservations",
                column: "ModifiedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ServiceId",
                schema: "Order",
                table: "Reservations",
                column: "ServiceId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Services_BusinessId",
                schema: "Order",
                table: "Services",
                column: "BusinessId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Services_CreatedById",
                schema: "Order",
                table: "Services",
                column: "CreatedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Services_ModifiedById",
                schema: "Order",
                table: "Services",
                column: "ModifiedById"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Reservations_ReservationId",
                schema: "Order",
                table: "Orders",
                column: "ReservationId",
                principalSchema: "Order",
                principalTable: "Reservations",
                principalColumn: "Id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Reservations_ReservationId",
                schema: "Order",
                table: "Orders"
            );

            migrationBuilder.DropTable(name: "Reservations", schema: "Order");

            migrationBuilder.DropTable(name: "Services", schema: "Order");

            migrationBuilder.DropIndex(name: "IX_Orders_ReservationId", schema: "Order", table: "Orders");

            migrationBuilder.DropColumn(name: "ReservationId", schema: "Order", table: "Orders");
        }
    }
}
