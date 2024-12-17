﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.DataAccess.Shared.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBusinessEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_Users_BusinessOwnerId",
                schema: "Business",
                table: "Businesses");

            migrationBuilder.AddColumn<int>(
                name: "EmployerBusinessId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployerBusinessId",
                table: "Users",
                column: "EmployerBusinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_Users_BusinessOwnerId",
                schema: "Business",
                table: "Businesses",
                column: "BusinessOwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Businesses_EmployerBusinessId",
                table: "Users",
                column: "EmployerBusinessId",
                principalSchema: "Business",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_Users_BusinessOwnerId",
                schema: "Business",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Businesses_EmployerBusinessId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EmployerBusinessId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmployerBusinessId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_Users_BusinessOwnerId",
                schema: "Business",
                table: "Businesses",
                column: "BusinessOwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}