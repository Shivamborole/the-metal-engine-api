using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InvoicingAPI.Migrations
{
    /// <inheritdoc />
    public partial class MenuItemsandDeliveryNoteRejectionNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "MenuItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeliveryChallan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChallanNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChallanDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    VehicleNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransporterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryChallan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryChallan_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryChallan_InvoiceDocuments_InvoiceDocumentId",
                        column: x => x.InvoiceDocumentId,
                        principalTable: "InvoiceDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryChallan_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryChallanCounters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentNumber = table.Column<int>(type: "int", nullable: false),
                    Prefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Padding = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryChallanCounters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RejectionNoteCounters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentNumber = table.Column<int>(type: "int", nullable: false),
                    Prefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Padding = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RejectionNoteCounters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryChallanItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliveryChallanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryChallanItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryChallanItems_DeliveryChallan_DeliveryChallanId",
                        column: x => x.DeliveryChallanId,
                        principalTable: "DeliveryChallan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryChallanItems_InvoiceItems_InvoiceItemId",
                        column: x => x.InvoiceItemId,
                        principalTable: "InvoiceItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryChallanItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RejectionNote",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliveryChallanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RejectionNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RejectionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RejectionNote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RejectionNote_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RejectionNote_DeliveryChallan_DeliveryChallanId",
                        column: x => x.DeliveryChallanId,
                        principalTable: "DeliveryChallan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RejectionNote_InvoiceDocuments_InvoiceDocumentId",
                        column: x => x.InvoiceDocumentId,
                        principalTable: "InvoiceDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RejectionNote_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RejectionNoteItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RejectionNoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliveryChallanItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliveryChallanItemsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RejectedQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RejectionNoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RejectionNoteItems_DeliveryChallanItems_DeliveryChallanItemsId",
                        column: x => x.DeliveryChallanItemsId,
                        principalTable: "DeliveryChallanItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RejectionNoteItems_InvoiceItems_InvoiceItemId",
                        column: x => x.InvoiceItemId,
                        principalTable: "InvoiceItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RejectionNoteItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RejectionNoteItems_RejectionNote_RejectionNoteId",
                        column: x => x.RejectionNoteId,
                        principalTable: "RejectionNote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Icon", "IsActive", "Order", "ParentId", "Route", "Title" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "dashboard", true, 1, null, "/dashboard", "Dashboard" },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "business", true, 2, null, "/companies", "Companies" },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "users", true, 3, null, "/customers", "Customers" },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "box", true, 4, null, "/products", "Products" },
                    { new Guid("10000000-0000-0000-0000-000000000005"), "sales", true, 5, null, "/sales", "Sales" },
                    { new Guid("10000000-0000-0000-0000-000000000006"), "settings", true, 6, null, "/settings", "Settings" }
                });

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 18, 23, 4, 611, DateTimeKind.Utc).AddTicks(5081));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 18, 23, 4, 611, DateTimeKind.Utc).AddTicks(5090));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 18, 23, 4, 611, DateTimeKind.Utc).AddTicks(5093));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 18, 23, 4, 611, DateTimeKind.Utc).AddTicks(5096));

            migrationBuilder.UpdateData(
                table: "RoleMasters",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 18, 23, 4, 611, DateTimeKind.Utc).AddTicks(5098));

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Icon", "IsActive", "Order", "ParentId", "Route", "Title" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), "invoice", true, 1, new Guid("10000000-0000-0000-0000-000000000005"), "/sales/invoices", "Invoices" },
                    { new Guid("20000000-0000-0000-0000-000000000002"), "truck", true, 2, new Guid("10000000-0000-0000-0000-000000000005"), "/sales/challans", "Delivery Challans" },
                    { new Guid("20000000-0000-0000-0000-000000000003"), "cancel", true, 3, new Guid("10000000-0000-0000-0000-000000000005"), "/sales/rejection-notes", "Rejection Notes" },
                    { new Guid("30000000-0000-0000-0000-000000000001"), "hash", true, 1, new Guid("10000000-0000-0000-0000-000000000006"), "/settings/invoice-number", "Invoice Number Format" },
                    { new Guid("30000000-0000-0000-0000-000000000002"), "file", true, 2, new Guid("10000000-0000-0000-0000-000000000006"), "/settings/pdf-template", "PDF Template" },
                    { new Guid("30000000-0000-0000-0000-000000000003"), "bank", true, 3, new Guid("10000000-0000-0000-0000-000000000006"), "/settings/bank-details", "Bank Details" },
                    { new Guid("30000000-0000-0000-0000-000000000004"), "qrcode", true, 4, new Guid("10000000-0000-0000-0000-000000000006"), "/settings/payment-qr", "Payment QR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_ParentId",
                table: "MenuItems",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallan_CreatedByUserId",
                table: "DeliveryChallan",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallan_CustomerId",
                table: "DeliveryChallan",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallan_InvoiceDocumentId",
                table: "DeliveryChallan",
                column: "InvoiceDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallanItems_DeliveryChallanId",
                table: "DeliveryChallanItems",
                column: "DeliveryChallanId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallanItems_InvoiceItemId",
                table: "DeliveryChallanItems",
                column: "InvoiceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallanItems_ProductId",
                table: "DeliveryChallanItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RejectionNote_CreatedByUserId",
                table: "RejectionNote",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RejectionNote_CustomerId",
                table: "RejectionNote",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RejectionNote_DeliveryChallanId",
                table: "RejectionNote",
                column: "DeliveryChallanId");

            migrationBuilder.CreateIndex(
                name: "IX_RejectionNote_InvoiceDocumentId",
                table: "RejectionNote",
                column: "InvoiceDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_RejectionNoteItems_DeliveryChallanItemsId",
                table: "RejectionNoteItems",
                column: "DeliveryChallanItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_RejectionNoteItems_InvoiceItemId",
                table: "RejectionNoteItems",
                column: "InvoiceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RejectionNoteItems_ProductId",
                table: "RejectionNoteItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RejectionNoteItems_RejectionNoteId",
                table: "RejectionNoteItems",
                column: "RejectionNoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_MenuItems_ParentId",
                table: "MenuItems",
                column: "ParentId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_MenuItems_ParentId",
                table: "MenuItems");

            migrationBuilder.DropTable(
                name: "DeliveryChallanCounters");

            migrationBuilder.DropTable(
                name: "RejectionNoteCounters");

            migrationBuilder.DropTable(
                name: "RejectionNoteItems");

            migrationBuilder.DropTable(
                name: "DeliveryChallanItems");

            migrationBuilder.DropTable(
                name: "RejectionNote");

            migrationBuilder.DropTable(
                name: "DeliveryChallan");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_ParentId",
                table: "MenuItems");

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"));

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "MenuItems");

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
    }
}
