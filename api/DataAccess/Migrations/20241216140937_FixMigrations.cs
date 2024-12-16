using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PointOfSale.DataAccess.Shared.Migrations
{
    /// <inheritdoc />
    public partial class FixMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "GrossPrice", schema: "Order", table: "OrderItemModifiers");

            migrationBuilder.EnsureSchema(name: "Business");

            migrationBuilder.EnsureSchema(name: "Payment");

            migrationBuilder.EnsureSchema(name: "ApplicationUsers");

            migrationBuilder.RenameColumn(
                name: "AppliedUnitAmount",
                schema: "Order",
                table: "OrderItemTaxes",
                newName: "AppliedAmount"
            );

            migrationBuilder.RenameColumn(
                name: "BaseUnitGrossPrice",
                schema: "Order",
                table: "OrderItems",
                newName: "BaseUnitPrice"
            );

            migrationBuilder.RenameColumn(
                name: "TaxTotal",
                schema: "Order",
                table: "OrderItemModifiers",
                newName: "Price"
            );

            migrationBuilder.RenameColumn(
                name: "AppliedUnitAmount",
                schema: "Order",
                table: "OrderItemDiscounts",
                newName: "AppliedAmount"
            );

            migrationBuilder.AddColumn<int>(
                name: "BusinessId",
                schema: "Order",
                table: "Taxes",
                type: "integer",
                nullable: false,
                defaultValue: 0
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
                name: "BusinessId",
                schema: "Order",
                table: "ServiceCharges",
                type: "integer",
                nullable: false,
                defaultValue: 0
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
                name: "BusinessId",
                schema: "Order",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0
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
                name: "BusinessId",
                schema: "Order",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0
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

            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "Order",
                table: "OrderItemDiscounts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: ""
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
                name: "BusinessId",
                schema: "Order",
                table: "Discounts",
                type: "integer",
                nullable: false,
                defaultValue: 0
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

            migrationBuilder.AddColumn<string>(
                name: "Target",
                schema: "Order",
                table: "Discounts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NormalizedName = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                }
            );

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                }
            );

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_UserTokens",
                        x => new
                        {
                            x.UserId,
                            x.LoginProvider,
                            x.Name,
                        }
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Businesses",
                schema: "Business",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    BusinessOwnerId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    TelephoneNumber = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Businesses_Users_BusinessOwnerId",
                        column: x => x.BusinessOwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Businesses_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Businesses_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "GiftCards",
                schema: "Payment",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UsedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiftCards_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_GiftCards_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "OrderDiscounts",
                schema: "Order",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    PricingStrategy = table.Column<string>(
                        type: "character varying(50)",
                        maxLength: 50,
                        nullable: false
                    ),
                    AppliedAmount = table.Column<decimal>(
                        type: "numeric(10,2)",
                        precision: 10,
                        scale: 2,
                        nullable: false
                    ),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDiscounts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Order",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_OrderDiscounts_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_OrderDiscounts_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "OrderPayments",
                schema: "Payment",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Method = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    GiftCardCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ExternalId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPayments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Order",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_OrderPayments_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_OrderPayments_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                schema: "ApplicationUsers",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    RefreshTokenHash = table.Column<string>(type: "text", nullable: false),
                    ApplicationUserId = table.Column<int>(type: "integer", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Tips",
                schema: "Payment",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tips_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Order",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Tips_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Tips_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Taxes_BusinessId",
                schema: "Order",
                table: "Taxes",
                column: "BusinessId"
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
                name: "IX_ServiceCharges_BusinessId",
                schema: "Order",
                table: "ServiceCharges",
                column: "BusinessId"
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
                name: "IX_Products_BusinessId",
                schema: "Order",
                table: "Products",
                column: "BusinessId"
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
                name: "IX_Orders_BusinessId",
                schema: "Order",
                table: "Orders",
                column: "BusinessId"
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
                name: "IX_Modifiers_BusinessId",
                schema: "Order",
                table: "Modifiers",
                column: "BusinessId"
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
                name: "IX_Discounts_BusinessId",
                schema: "Order",
                table: "Discounts",
                column: "BusinessId"
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
                name: "IX_Businesses_BusinessOwnerId",
                schema: "Business",
                table: "Businesses",
                column: "BusinessOwnerId",
                unique: true
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

            migrationBuilder.CreateIndex(
                name: "IX_GiftCards_Code",
                schema: "Payment",
                table: "GiftCards",
                column: "Code",
                unique: true
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
                name: "IX_OrderDiscounts_CreatedById",
                schema: "Order",
                table: "OrderDiscounts",
                column: "CreatedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderDiscounts_ModifiedById",
                schema: "Order",
                table: "OrderDiscounts",
                column: "ModifiedById"
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderDiscounts_OrderId",
                schema: "Order",
                table: "OrderDiscounts",
                column: "OrderId"
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
                name: "IX_OrderPayments_OrderId",
                schema: "Payment",
                table: "OrderPayments",
                column: "OrderId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_ApplicationUserId",
                schema: "ApplicationUsers",
                table: "RefreshTokens",
                column: "ApplicationUserId"
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

            migrationBuilder.CreateIndex(name: "IX_Tips_OrderId", schema: "Payment", table: "Tips", column: "OrderId");

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
                name: "FK_Taxes_Businesses_BusinessId",
                schema: "Order",
                table: "Taxes",
                column: "BusinessId",
                principalSchema: "Business",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Businesses_BusinessId",
                schema: "Order",
                table: "Discounts"
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
                name: "FK_Modifiers_Businesses_BusinessId",
                schema: "Order",
                table: "Modifiers"
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

            migrationBuilder.DropForeignKey(name: "FK_Orders_Businesses_BusinessId", schema: "Order", table: "Orders");

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

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Businesses_BusinessId",
                schema: "Order",
                table: "Products"
            );

            migrationBuilder.DropForeignKey(name: "FK_Products_Users_CreatedById", schema: "Order", table: "Products");

            migrationBuilder.DropForeignKey(name: "FK_Products_Users_ModifiedById", schema: "Order", table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCharges_Businesses_BusinessId",
                schema: "Order",
                table: "ServiceCharges"
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

            migrationBuilder.DropForeignKey(name: "FK_Taxes_Businesses_BusinessId", schema: "Order", table: "Taxes");

            migrationBuilder.DropForeignKey(name: "FK_Taxes_Users_CreatedById", schema: "Order", table: "Taxes");

            migrationBuilder.DropForeignKey(name: "FK_Taxes_Users_ModifiedById", schema: "Order", table: "Taxes");

            migrationBuilder.DropTable(name: "Businesses", schema: "Business");

            migrationBuilder.DropTable(name: "GiftCards", schema: "Payment");

            migrationBuilder.DropTable(name: "OrderDiscounts", schema: "Order");

            migrationBuilder.DropTable(name: "OrderPayments", schema: "Payment");

            migrationBuilder.DropTable(name: "RefreshTokens", schema: "ApplicationUsers");

            migrationBuilder.DropTable(name: "RoleClaims");

            migrationBuilder.DropTable(name: "Roles");

            migrationBuilder.DropTable(name: "Tips", schema: "Payment");

            migrationBuilder.DropTable(name: "UserClaims");

            migrationBuilder.DropTable(name: "UserLogins");

            migrationBuilder.DropTable(name: "UserRoles");

            migrationBuilder.DropTable(name: "UserTokens");

            migrationBuilder.DropTable(name: "Users");

            migrationBuilder.DropIndex(name: "IX_Taxes_BusinessId", schema: "Order", table: "Taxes");

            migrationBuilder.DropIndex(name: "IX_Taxes_CreatedById", schema: "Order", table: "Taxes");

            migrationBuilder.DropIndex(name: "IX_Taxes_ModifiedById", schema: "Order", table: "Taxes");

            migrationBuilder.DropIndex(name: "IX_ServiceCharges_BusinessId", schema: "Order", table: "ServiceCharges");

            migrationBuilder.DropIndex(name: "IX_ServiceCharges_CreatedById", schema: "Order", table: "ServiceCharges");

            migrationBuilder.DropIndex(
                name: "IX_ServiceCharges_ModifiedById",
                schema: "Order",
                table: "ServiceCharges"
            );

            migrationBuilder.DropIndex(name: "IX_Products_BusinessId", schema: "Order", table: "Products");

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

            migrationBuilder.DropIndex(name: "IX_Orders_BusinessId", schema: "Order", table: "Orders");

            migrationBuilder.DropIndex(name: "IX_Orders_CreatedById", schema: "Order", table: "Orders");

            migrationBuilder.DropIndex(name: "IX_Orders_ModifiedById", schema: "Order", table: "Orders");

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

            migrationBuilder.DropIndex(name: "IX_Modifiers_BusinessId", schema: "Order", table: "Modifiers");

            migrationBuilder.DropIndex(name: "IX_Modifiers_CreatedById", schema: "Order", table: "Modifiers");

            migrationBuilder.DropIndex(name: "IX_Modifiers_ModifiedById", schema: "Order", table: "Modifiers");

            migrationBuilder.DropIndex(name: "IX_Discounts_BusinessId", schema: "Order", table: "Discounts");

            migrationBuilder.DropIndex(name: "IX_Discounts_CreatedById", schema: "Order", table: "Discounts");

            migrationBuilder.DropIndex(name: "IX_Discounts_ModifiedById", schema: "Order", table: "Discounts");

            migrationBuilder.DropColumn(name: "BusinessId", schema: "Order", table: "Taxes");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Order", table: "Taxes");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Order", table: "Taxes");

            migrationBuilder.DropColumn(name: "BusinessId", schema: "Order", table: "ServiceCharges");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Order", table: "ServiceCharges");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Order", table: "ServiceCharges");

            migrationBuilder.DropColumn(name: "BusinessId", schema: "Order", table: "Products");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Order", table: "Products");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Order", table: "Products");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Order", table: "OrderServiceCharges");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Order", table: "OrderServiceCharges");

            migrationBuilder.DropColumn(name: "BusinessId", schema: "Order", table: "Orders");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Order", table: "Orders");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Order", table: "Orders");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Order", table: "OrderItemTaxes");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Order", table: "OrderItemTaxes");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Order", table: "OrderItems");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Order", table: "OrderItems");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Order", table: "OrderItemModifiers");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Order", table: "OrderItemModifiers");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Order", table: "OrderItemDiscounts");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Order", table: "OrderItemDiscounts");

            migrationBuilder.DropColumn(name: "Type", schema: "Order", table: "OrderItemDiscounts");

            migrationBuilder.DropColumn(name: "BusinessId", schema: "Order", table: "Modifiers");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Order", table: "Modifiers");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Order", table: "Modifiers");

            migrationBuilder.DropColumn(name: "BusinessId", schema: "Order", table: "Discounts");

            migrationBuilder.DropColumn(name: "CreatedById", schema: "Order", table: "Discounts");

            migrationBuilder.DropColumn(name: "ModifiedById", schema: "Order", table: "Discounts");

            migrationBuilder.DropColumn(name: "Target", schema: "Order", table: "Discounts");

            migrationBuilder.RenameColumn(
                name: "AppliedAmount",
                schema: "Order",
                table: "OrderItemTaxes",
                newName: "AppliedUnitAmount"
            );

            migrationBuilder.RenameColumn(
                name: "BaseUnitPrice",
                schema: "Order",
                table: "OrderItems",
                newName: "BaseUnitGrossPrice"
            );

            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "Order",
                table: "OrderItemModifiers",
                newName: "TaxTotal"
            );

            migrationBuilder.RenameColumn(
                name: "AppliedAmount",
                schema: "Order",
                table: "OrderItemDiscounts",
                newName: "AppliedUnitAmount"
            );

            migrationBuilder.AddColumn<decimal>(
                name: "GrossPrice",
                schema: "Order",
                table: "OrderItemModifiers",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m
            );
        }
    }
}
