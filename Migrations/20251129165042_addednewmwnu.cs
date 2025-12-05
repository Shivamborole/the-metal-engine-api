using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InvoicingAPI.Migrations
{
    /// <inheritdoc />
    public partial class addednewmwnu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("0423668a-2797-423a-a5ca-73febce54b4d"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("27d9ea6f-fdb3-427b-a328-9dd176d1d663"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("d1ce8c83-986d-4158-b608-24562c5bd397"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("eb889e04-8fcb-4cf3-b7eb-f42efe6b9810"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("ee5e62de-c65c-4b1b-87f7-d9908ff22a4d"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("0423668a-2797-423a-a5ca-73febce54b4d"), "users", true, 3, "/customers", "Customers" },
                    { new Guid("27d9ea6f-fdb3-427b-a328-9dd176d1d663"), "dashboard", true, 1, "/dashboard", "Dashboard" },
                    { new Guid("d1ce8c83-986d-4158-b608-24562c5bd397"), "business", true, 2, "/companies", "Companies" },
                    { new Guid("eb889e04-8fcb-4cf3-b7eb-f42efe6b9810"), "box", true, 4, "/products", "Products" },
                    { new Guid("ee5e62de-c65c-4b1b-87f7-d9908ff22a4d"), "invoice", true, 5, "/invoices", "Invoices" }
                });

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 17, 14, 33, 436, DateTimeKind.Utc).AddTicks(6959));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 17, 14, 33, 436, DateTimeKind.Utc).AddTicks(6965));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 17, 14, 33, 436, DateTimeKind.Utc).AddTicks(6967));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 17, 14, 33, 436, DateTimeKind.Utc).AddTicks(6969));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 17, 14, 33, 436, DateTimeKind.Utc).AddTicks(6972));
        }
    }
}
