using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InvoicingAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRemanings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("238707aa-67f0-4f33-85c1-43f9e43ad303"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("34960b68-b615-4292-8d38-29f879b2e27b"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("6787cdbf-565b-4ba2-904c-01f417062a62"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("6d3c087d-bdf9-415e-ba4d-aaf330839d3a"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("7a7a95a0-3a2c-47f3-a6a0-ee8f6eb29c4b"));

            migrationBuilder.AddColumn<decimal>(
                name: "MaterialQuantity",
                table: "ProductBoms",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId1",
                table: "ProductBoms",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentStock",
                table: "Materials",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "MaterialPurchases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "CurrentMonth",
                table: "InvoiceNumberSettings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Template",
                table: "InvoiceNumberSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UnitConversions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FromUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConversionRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitConversions", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductBoms_ProductId1",
                table: "ProductBoms",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBoms_Products_ProductId1",
                table: "ProductBoms",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBoms_Products_ProductId1",
                table: "ProductBoms");

            migrationBuilder.DropTable(
                name: "UnitConversions");

            migrationBuilder.DropIndex(
                name: "IX_ProductBoms_ProductId1",
                table: "ProductBoms");

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

            migrationBuilder.DropColumn(
                name: "MaterialQuantity",
                table: "ProductBoms");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductBoms");

            migrationBuilder.DropColumn(
                name: "CurrentStock",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "MaterialPurchases");

            migrationBuilder.DropColumn(
                name: "Template",
                table: "InvoiceNumberSettings");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentMonth",
                table: "InvoiceNumberSettings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Icon", "IsActive", "Order", "Route", "Title" },
                values: new object[,]
                {
                    { new Guid("238707aa-67f0-4f33-85c1-43f9e43ad303"), "dashboard", true, 1, "/dashboard", "Dashboard" },
                    { new Guid("34960b68-b615-4292-8d38-29f879b2e27b"), "users", true, 3, "/customers", "Customers" },
                    { new Guid("6787cdbf-565b-4ba2-904c-01f417062a62"), "invoice", true, 5, "/invoices", "Invoices" },
                    { new Guid("6d3c087d-bdf9-415e-ba4d-aaf330839d3a"), "business", true, 2, "/companies", "Companies" },
                    { new Guid("7a7a95a0-3a2c-47f3-a6a0-ee8f6eb29c4b"), "box", true, 4, "/products", "Products" }
                });

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 13, 57, 50, 302, DateTimeKind.Utc).AddTicks(1794));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 13, 57, 50, 302, DateTimeKind.Utc).AddTicks(1806));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 13, 57, 50, 302, DateTimeKind.Utc).AddTicks(1812));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 13, 57, 50, 302, DateTimeKind.Utc).AddTicks(1817));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 13, 57, 50, 302, DateTimeKind.Utc).AddTicks(1823));
        }
    }
}
