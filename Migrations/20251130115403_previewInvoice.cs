using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InvoicingAPI.Migrations
{
    /// <inheritdoc />
    public partial class previewInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
