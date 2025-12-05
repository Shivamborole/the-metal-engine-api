using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InvoicingAPI.Migrations
{
    /// <inheritdoc />
    public partial class setting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("12ea07b9-f1f6-4212-90df-0c10d9c36aa3"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("2f4e0f7d-87ca-47c5-a717-85b51789fd8a"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("78d3d69f-2b93-4183-876a-161c335190d5"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("c34407a9-0ae8-4ffe-92a4-3c5c08e3dadc"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("d7ddb82c-cbab-4d44-b077-ef37cdcfae1c"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("da052997-b763-44e3-b4e9-56a9215c0fea"));

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Icon", "IsActive", "Order", "Route", "Title" },
                values: new object[,]
                {
                    { new Guid("38a31a14-1f06-49cf-8065-936e037797d2"), "business", true, 2, "/companies", "Companies" },
                    { new Guid("3c9400eb-42b3-403e-baee-f21931f35a40"), "settings", true, 6, "/settings", "Settings" },
                    { new Guid("3e4c4e7c-2965-427b-9cf0-6c94bf7eaf9c"), "box", true, 4, "/products", "Products" },
                    { new Guid("7798bb0c-1b5a-44b3-9d89-1956d34b5f0a"), "invoice", true, 5, "/invoices", "Invoices" },
                    { new Guid("7c881fc5-7460-43b4-8480-3d4e63d54e98"), "dashboard", true, 1, "/dashboard", "Dashboard" },
                    { new Guid("e728b525-e366-4f0d-9cf6-c2871e3dd3b4"), "users", true, 3, "/customers", "Customers" }
                });

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 29, 17, 12, 28, 267, DateTimeKind.Utc).AddTicks(4795));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 29, 17, 12, 28, 267, DateTimeKind.Utc).AddTicks(4799));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 29, 17, 12, 28, 267, DateTimeKind.Utc).AddTicks(4801));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 29, 17, 12, 28, 267, DateTimeKind.Utc).AddTicks(4804));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 29, 17, 12, 28, 267, DateTimeKind.Utc).AddTicks(4806));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("38a31a14-1f06-49cf-8065-936e037797d2"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("3c9400eb-42b3-403e-baee-f21931f35a40"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("3e4c4e7c-2965-427b-9cf0-6c94bf7eaf9c"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("7798bb0c-1b5a-44b3-9d89-1956d34b5f0a"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("7c881fc5-7460-43b4-8480-3d4e63d54e98"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("e728b525-e366-4f0d-9cf6-c2871e3dd3b4"));

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Icon", "IsActive", "Order", "Route", "Title" },
                values: new object[,]
                {
                    { new Guid("12ea07b9-f1f6-4212-90df-0c10d9c36aa3"), "box", true, 4, "/products", "Products" },
                    { new Guid("2f4e0f7d-87ca-47c5-a717-85b51789fd8a"), "users", true, 3, "/customers", "Customers" },
                    { new Guid("78d3d69f-2b93-4183-876a-161c335190d5"), "settings", true, 6, "/settings", "Settings" },
                    { new Guid("c34407a9-0ae8-4ffe-92a4-3c5c08e3dadc"), "business", true, 2, "/companies", "Companies" },
                    { new Guid("d7ddb82c-cbab-4d44-b077-ef37cdcfae1c"), "invoice", true, 5, "/invoices", "Invoices" },
                    { new Guid("da052997-b763-44e3-b4e9-56a9215c0fea"), "dashboard", true, 1, "/dashboard", "Dashboard" }
                });

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 29, 16, 50, 39, 965, DateTimeKind.Utc).AddTicks(8966));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 29, 16, 50, 39, 965, DateTimeKind.Utc).AddTicks(8973));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 29, 16, 50, 39, 965, DateTimeKind.Utc).AddTicks(8976));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 29, 16, 50, 39, 965, DateTimeKind.Utc).AddTicks(8977));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 29, 16, 50, 39, 965, DateTimeKind.Utc).AddTicks(8979));
        }
    }
}
