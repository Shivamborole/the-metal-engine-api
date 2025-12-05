using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InvoicingAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddProductMaterialBomModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItems_Products_ProductId1",
                table: "InvoiceItems");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItems_ProductId1",
                table: "InvoiceItems");

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

            migrationBuilder.DropColumn(
                name: "HsnCode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "InvoiceItems");

            migrationBuilder.AlterColumn<decimal>(
                name: "GstRate",
                table: "Products",
                type: "decimal(5,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HsnOrSac",
                table: "Products",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxInclusive",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProductType",
                table: "Products",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PurchasePrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SellingPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "SellingUnit",
                table: "Products",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sku",
                table: "Products",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BaseUnit = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    OpeningStockQty = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    CurrentStockQty = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    ReorderLevel = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    AvgCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialPurchases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialPurchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialPurchases_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialPurchases_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductBoms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QtyRequiredPerUnit = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    WastePercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBoms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductBoms_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductBoms_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductBoms_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityChange = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SourceReference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransactions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockTransactions_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_MaterialPurchases_CompanyId",
                table: "MaterialPurchases",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialPurchases_MaterialId",
                table: "MaterialPurchases",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_CompanyId",
                table: "Materials",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBoms_CompanyId",
                table: "ProductBoms",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBoms_MaterialId",
                table: "ProductBoms",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBoms_ProductId",
                table: "ProductBoms",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_CompanyId",
                table: "StockTransactions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_MaterialId",
                table: "StockTransactions",
                column: "MaterialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialPurchases");

            migrationBuilder.DropTable(
                name: "ProductBoms");

            migrationBuilder.DropTable(
                name: "StockTransactions");

            migrationBuilder.DropTable(
                name: "Materials");

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

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "HsnOrSac",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsTaxInclusive",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SellingPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SellingUnit",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Sku",
                table: "Products");

            migrationBuilder.AlterColumn<decimal>(
                name: "GstRate",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "HsnCode",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId1",
                table: "InvoiceItems",
                type: "uniqueidentifier",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_ProductId1",
                table: "InvoiceItems",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItems_Products_ProductId1",
                table: "InvoiceItems",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
