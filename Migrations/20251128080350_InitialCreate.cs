using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InvoicingAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("09e9e45b-7d74-405f-9708-07c485c756d1"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("187cd849-5336-4bb1-b037-c5283752c649"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("3637a8b9-4746-4dc4-ad7c-8754b775c0e7"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("3a003b1e-66c0-47ff-b3c2-78f1c2bc6eb4"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("9c6e53d5-89cd-4784-a739-a17662824ab0"));

            migrationBuilder.DropColumn(
                name: "Role",
                table: "UserCompanyMaps");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId1",
                table: "UserCompanyMaps",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "UserCompanyMaps",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "UserCompanyMaps",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoleMasters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSystemRole = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMasters", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Icon", "IsActive", "Order", "Route", "Title" },
                values: new object[,]
                {
                    { new Guid("2566e499-a660-4cb5-a2ea-30a1931eb8ef"), "box", true, 4, "/products", "Products" },
                    { new Guid("48e8bb12-087f-4a2c-a68d-01feb4751622"), "dashboard", true, 1, "/dashboard", "Dashboard" },
                    { new Guid("5590bcc5-585a-4841-8f04-ee6d21703ef5"), "invoice", true, 5, "/invoices", "Invoices" },
                    { new Guid("6674bb8f-f4eb-4b38-b643-cc909bb2a49e"), "users", true, 3, "/customers", "Customers" },
                    { new Guid("db2e0864-d08c-4de6-bec5-6911f1dbba15"), "business", true, 2, "/companies", "Companies" }
                });

            migrationBuilder.InsertData(
                table: "RoleMasters",
                columns: new[] { "Id", "CreatedAt", "Description", "IsSystemRole", "RoleName" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 11, 28, 8, 3, 48, 844, DateTimeKind.Utc).AddTicks(8516), "Full access to the company", true, "Owner" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 11, 28, 8, 3, 48, 844, DateTimeKind.Utc).AddTicks(8525), "Manage all modules except company deletion", true, "Admin" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 11, 28, 8, 3, 48, 844, DateTimeKind.Utc).AddTicks(8529), "Finance, GST, invoices, expenses", false, "Accountant" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 11, 28, 8, 3, 48, 844, DateTimeKind.Utc).AddTicks(8531), "Create invoices, manage customers", false, "Sales Manager" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 11, 28, 8, 3, 48, 844, DateTimeKind.Utc).AddTicks(8533), "Read-only access", false, "Viewer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCompanyMaps_CompanyId1",
                table: "UserCompanyMaps",
                column: "CompanyId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompanyMaps_RoleId",
                table: "UserCompanyMaps",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompanyMaps_UserId1",
                table: "UserCompanyMaps",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCompanyMaps_Companies_CompanyId1",
                table: "UserCompanyMaps",
                column: "CompanyId1",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCompanyMaps_RoleMasters_RoleId",
                table: "UserCompanyMaps",
                column: "RoleId",
                principalTable: "RoleMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCompanyMaps_Users_UserId1",
                table: "UserCompanyMaps",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCompanyMaps_Companies_CompanyId1",
                table: "UserCompanyMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCompanyMaps_RoleMasters_RoleId",
                table: "UserCompanyMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCompanyMaps_Users_UserId1",
                table: "UserCompanyMaps");

            migrationBuilder.DropTable(
                name: "RoleMasters");

            migrationBuilder.DropIndex(
                name: "IX_UserCompanyMaps_CompanyId1",
                table: "UserCompanyMaps");

            migrationBuilder.DropIndex(
                name: "IX_UserCompanyMaps_RoleId",
                table: "UserCompanyMaps");

            migrationBuilder.DropIndex(
                name: "IX_UserCompanyMaps_UserId1",
                table: "UserCompanyMaps");

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("2566e499-a660-4cb5-a2ea-30a1931eb8ef"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("48e8bb12-087f-4a2c-a68d-01feb4751622"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("5590bcc5-585a-4841-8f04-ee6d21703ef5"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("6674bb8f-f4eb-4b38-b643-cc909bb2a49e"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("db2e0864-d08c-4de6-bec5-6911f1dbba15"));

            migrationBuilder.DropColumn(
                name: "CompanyId1",
                table: "UserCompanyMaps");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "UserCompanyMaps");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserCompanyMaps");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "UserCompanyMaps",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Icon", "IsActive", "Order", "Route", "Title" },
                values: new object[,]
                {
                    { new Guid("09e9e45b-7d74-405f-9708-07c485c756d1"), "invoice", true, 5, "/invoices", "Invoices" },
                    { new Guid("187cd849-5336-4bb1-b037-c5283752c649"), "box", true, 4, "/products", "Products" },
                    { new Guid("3637a8b9-4746-4dc4-ad7c-8754b775c0e7"), "users", true, 3, "/customers", "Customers" },
                    { new Guid("3a003b1e-66c0-47ff-b3c2-78f1c2bc6eb4"), "dashboard", true, 1, "/dashboard", "Dashboard" },
                    { new Guid("9c6e53d5-89cd-4784-a739-a17662824ab0"), "business", true, 2, "/companies", "Companies" }
                });
        }
    }
}
