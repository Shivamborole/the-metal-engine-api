using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InvoicingAPI.Migrations
{
    /// <inheritdoc />
    public partial class QuotationModuleAndStatusadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("132112a7-19c8-4ea9-b2bb-28155c60301a"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("7555d9f8-96f4-4dd6-8005-014a767a630b"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("e41d525e-0be4-4d7a-b1e0-17b1459c4452"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("ef556192-3c67-4583-bd3b-5bb29fabea5f"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("fb72d221-d3de-4338-8f32-27094f43d9ea"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("fd6106f8-18fc-4356-9022-49103ff87a9e"));

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "InvoiceDocuments",
                type: "int",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "InvoiceDocuments");

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Icon", "IsActive", "Order", "Route", "Title" },
                values: new object[,]
                {
                    { new Guid("132112a7-19c8-4ea9-b2bb-28155c60301a"), "dashboard", true, 1, "/dashboard", "Dashboard" },
                    { new Guid("7555d9f8-96f4-4dd6-8005-014a767a630b"), "business", true, 2, "/companies", "Companies" },
                    { new Guid("e41d525e-0be4-4d7a-b1e0-17b1459c4452"), "invoice", true, 5, "/invoices", "Invoices" },
                    { new Guid("ef556192-3c67-4583-bd3b-5bb29fabea5f"), "box", true, 4, "/products", "Products" },
                    { new Guid("fb72d221-d3de-4338-8f32-27094f43d9ea"), "settings", true, 6, "/settings", "Settings" },
                    { new Guid("fd6106f8-18fc-4356-9022-49103ff87a9e"), "users", true, 3, "/customers", "Customers" }
                });

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 11, 54, 1, 191, DateTimeKind.Utc).AddTicks(9652));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 11, 54, 1, 191, DateTimeKind.Utc).AddTicks(9658));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 11, 54, 1, 191, DateTimeKind.Utc).AddTicks(9661));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 11, 54, 1, 191, DateTimeKind.Utc).AddTicks(9664));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 11, 54, 1, 191, DateTimeKind.Utc).AddTicks(9668));
        }
    }
}
