using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InvoicingAPI.Migrations
{
    /// <inheritdoc />
    public partial class remaningchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("07868fa6-6dbe-4572-97d5-00a54fb216f5"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("1a7649f0-b485-43d8-ab45-22f04763936d"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("37fc26e5-6513-43ae-832d-749b084baa61"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("50e94990-2345-4a26-8956-9279592f821d"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("5a124045-6396-489f-a0b1-e45eda3245a1"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("debf0a17-c081-493a-a1b0-72603d35fd34"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Icon", "IsActive", "Order", "Route", "Title" },
                values: new object[,]
                {
                    { new Guid("07868fa6-6dbe-4572-97d5-00a54fb216f5"), "settings", true, 6, "/settings", "Settings" },
                    { new Guid("1a7649f0-b485-43d8-ab45-22f04763936d"), "business", true, 2, "/companies", "Companies" },
                    { new Guid("37fc26e5-6513-43ae-832d-749b084baa61"), "dashboard", true, 1, "/dashboard", "Dashboard" },
                    { new Guid("50e94990-2345-4a26-8956-9279592f821d"), "box", true, 4, "/products", "Products" },
                    { new Guid("5a124045-6396-489f-a0b1-e45eda3245a1"), "invoice", true, 5, "/invoices", "Invoices" },
                    { new Guid("debf0a17-c081-493a-a1b0-72603d35fd34"), "users", true, 3, "/customers", "Customers" }
                });

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 17, 6, 53, 925, DateTimeKind.Utc).AddTicks(1002));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 17, 6, 53, 925, DateTimeKind.Utc).AddTicks(1005));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 17, 6, 53, 925, DateTimeKind.Utc).AddTicks(1007));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 17, 6, 53, 925, DateTimeKind.Utc).AddTicks(1008));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 17, 6, 53, 925, DateTimeKind.Utc).AddTicks(1010));
        }
    }
}
