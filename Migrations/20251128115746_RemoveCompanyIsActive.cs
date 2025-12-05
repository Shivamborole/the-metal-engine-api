using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InvoicingAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCompanyIsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "IsActive",
                table: "Companies");

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Icon", "IsActive", "Order", "Route", "Title" },
                values: new object[,]
                {
                    { new Guid("0302f9c5-8abe-477a-ad54-77e2a6fb35b5"), "box", true, 4, "/products", "Products" },
                    { new Guid("29b88dbd-43a2-4925-bc58-a69b54ae0b7e"), "users", true, 3, "/customers", "Customers" },
                    { new Guid("44be7796-d841-4e36-82f0-ef9b58efc26d"), "dashboard", true, 1, "/dashboard", "Dashboard" },
                    { new Guid("82d003c1-1253-4558-9433-dfc564ed2e86"), "invoice", true, 5, "/invoices", "Invoices" },
                    { new Guid("c5175c6c-a756-428c-af0b-fb0d61685f74"), "business", true, 2, "/companies", "Companies" }
                });

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 11, 57, 42, 870, DateTimeKind.Utc).AddTicks(8565));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 11, 57, 42, 870, DateTimeKind.Utc).AddTicks(8576));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 11, 57, 42, 870, DateTimeKind.Utc).AddTicks(8581));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 11, 57, 42, 870, DateTimeKind.Utc).AddTicks(8585));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 11, 57, 42, 870, DateTimeKind.Utc).AddTicks(8589));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("0302f9c5-8abe-477a-ad54-77e2a6fb35b5"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("29b88dbd-43a2-4925-bc58-a69b54ae0b7e"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("44be7796-d841-4e36-82f0-ef9b58efc26d"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("82d003c1-1253-4558-9433-dfc564ed2e86"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("c5175c6c-a756-428c-af0b-fb0d61685f74"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 8, 3, 48, 844, DateTimeKind.Utc).AddTicks(8516));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 8, 3, 48, 844, DateTimeKind.Utc).AddTicks(8525));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 8, 3, 48, 844, DateTimeKind.Utc).AddTicks(8529));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 8, 3, 48, 844, DateTimeKind.Utc).AddTicks(8531));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 8, 3, 48, 844, DateTimeKind.Utc).AddTicks(8533));
        }
    }
}
