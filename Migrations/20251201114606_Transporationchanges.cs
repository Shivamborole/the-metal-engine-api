using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InvoicingAPI.Migrations
{
    /// <inheritdoc />
    public partial class Transporationchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("03ee4fcd-e8ce-4506-9961-38e1dbb1d3c2"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("20f97ad5-7bd1-4c71-8b28-749480dda7c8"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("25e2789b-f737-4a8a-b455-e1e4f128a02f"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("2fc19726-8f25-4b94-aa11-28663a194ac5"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("592c61f3-db99-471c-92af-b913f90ad02e"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("8972dfef-450b-4287-8fd7-d7d227c17f92"));

            migrationBuilder.AddColumn<decimal>(
                name: "LoadingCharges",
                table: "InvoiceDocuments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TransportCharges",
                table: "InvoiceDocuments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Icon", "IsActive", "Order", "Route", "Title" },
                values: new object[,]
                {
                    { new Guid("2332d6d5-ce09-4c3f-92aa-17ef6e009312"), "invoice", true, 5, "/invoices", "Invoices" },
                    { new Guid("7bafbb95-c0c7-455e-a1bb-feea7c1132c8"), "users", true, 3, "/customers", "Customers" },
                    { new Guid("8374898c-4c51-4c93-8016-05bb3acfe0f8"), "dashboard", true, 1, "/dashboard", "Dashboard" },
                    { new Guid("e1b339fa-b86a-4521-8b62-3831ff3b95fc"), "settings", true, 6, "/settings", "Settings" },
                    { new Guid("ee83c9b1-3722-4628-8136-94cefd54c4ce"), "box", true, 4, "/products", "Products" },
                    { new Guid("fccf862c-158a-43c9-999a-4b569242dabd"), "business", true, 2, "/companies", "Companies" }
                });

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 11, 46, 5, 37, DateTimeKind.Utc).AddTicks(2359));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 11, 46, 5, 37, DateTimeKind.Utc).AddTicks(2372));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 11, 46, 5, 37, DateTimeKind.Utc).AddTicks(2376));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 11, 46, 5, 37, DateTimeKind.Utc).AddTicks(2379));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 11, 46, 5, 37, DateTimeKind.Utc).AddTicks(2382));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("2332d6d5-ce09-4c3f-92aa-17ef6e009312"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("7bafbb95-c0c7-455e-a1bb-feea7c1132c8"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("8374898c-4c51-4c93-8016-05bb3acfe0f8"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("e1b339fa-b86a-4521-8b62-3831ff3b95fc"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("ee83c9b1-3722-4628-8136-94cefd54c4ce"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("fccf862c-158a-43c9-999a-4b569242dabd"));

            migrationBuilder.DropColumn(
                name: "LoadingCharges",
                table: "InvoiceDocuments");

            migrationBuilder.DropColumn(
                name: "TransportCharges",
                table: "InvoiceDocuments");

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Icon", "IsActive", "Order", "Route", "Title" },
                values: new object[,]
                {
                    { new Guid("03ee4fcd-e8ce-4506-9961-38e1dbb1d3c2"), "invoice", true, 5, "/invoices", "Invoices" },
                    { new Guid("20f97ad5-7bd1-4c71-8b28-749480dda7c8"), "settings", true, 6, "/settings", "Settings" },
                    { new Guid("25e2789b-f737-4a8a-b455-e1e4f128a02f"), "dashboard", true, 1, "/dashboard", "Dashboard" },
                    { new Guid("2fc19726-8f25-4b94-aa11-28663a194ac5"), "users", true, 3, "/customers", "Customers" },
                    { new Guid("592c61f3-db99-471c-92af-b913f90ad02e"), "box", true, 4, "/products", "Products" },
                    { new Guid("8972dfef-450b-4287-8fd7-d7d227c17f92"), "business", true, 2, "/companies", "Companies" }
                });

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 17, 47, 1, 489, DateTimeKind.Utc).AddTicks(6383));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 17, 47, 1, 489, DateTimeKind.Utc).AddTicks(6390));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 17, 47, 1, 489, DateTimeKind.Utc).AddTicks(6392));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 17, 47, 1, 489, DateTimeKind.Utc).AddTicks(6394));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 17, 47, 1, 489, DateTimeKind.Utc).AddTicks(6396));
        }
    }
}
