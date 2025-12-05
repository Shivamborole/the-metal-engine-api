
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using global::InvoicingAPI.Domain.Entities;
    using InvoicingAPI.Domain.Entities;
    using QuestPDF.Fluent;
    using QuestPDF.Helpers;
    using QuestPDF.Infrastructure;

    namespace InvoicingAPI.Application.Helpers
    {
    public class InvoicePdfDocument : IDocument
    {
        private readonly InvoiceDocument _invoice;
        private readonly InvoicePdfSettings _settings;
        private readonly byte[]? _qrBytes;
        private readonly byte[]? _logoBytes;   // optional, if you later load logo as bytes

        public InvoicePdfDocument(
            InvoiceDocument invoice,
            InvoicePdfSettings settings,
            byte[]? qrBytes = null,
            byte[]? logoBytes = null)
        {
            _invoice = invoice;
            _settings = settings;
            _qrBytes = qrBytes;
            _logoBytes = logoBytes;
        }

        public DocumentMetadata GetMetadata() => new DocumentMetadata
        {
            Title = _invoice.InvoiceNumber ?? "Invoice",
            Author = _settings.CompanyDisplayName ?? "Invoicing"
        };

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.DefaultTextStyle(x => x.FontSize(10));
                page.PageColor(Colors.White);

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().Element(ComposeFooter);
            });
        }

        // =====================================================================
        // HEADER
        // =====================================================================
        void ComposeHeader(IContainer container)
        {
            var primary = _settings.PrimaryColorHex;

            container.Row(row =>
            {
                // LEFT: Logo + company
                row.RelativeItem().Column(col =>
                {
                    if (_logoBytes != null && _logoBytes.Length > 0)
                    {
                        col.Item().Height(40).Width(120).Image(_logoBytes);
                        //col.Item().Spacing(5);
                        col.Item().PaddingBottom(5);

                    }

                    col.Item().Text(_settings.CompanyDisplayName ?? "Your Company")
                        .SemiBold().FontSize(16).FontColor(primary);

                    if (!string.IsNullOrWhiteSpace(_settings.CompanyAddressLine1))
                        col.Item().Text(_settings.CompanyAddressLine1);

                    if (!string.IsNullOrWhiteSpace(_settings.CompanyAddressLine2))
                        col.Item().Text(_settings.CompanyAddressLine2);

                    if (!string.IsNullOrWhiteSpace(_settings.CompanyGstin))
                        col.Item().Text($"GSTIN: {_settings.CompanyGstin}");

                    if (!string.IsNullOrWhiteSpace(_settings.CompanyPhone) ||
                        !string.IsNullOrWhiteSpace(_settings.CompanyEmail))
                    {
                        col.Item().Text(text =>
                        {
                            if (!string.IsNullOrWhiteSpace(_settings.CompanyPhone))
                                text.Span($"Phone: {_settings.CompanyPhone}  ");
                            if (!string.IsNullOrWhiteSpace(_settings.CompanyEmail))
                                text.Span($"Email: {_settings.CompanyEmail}");
                        });
                    }
                });

                // RIGHT: Invoice meta
                row.ConstantItem(200).Column(col =>
                {
                    var docLabel = _invoice.DocumentType == DocumentType.Invoice ? "INVOICE" : "QUOTATION";

                    col.Item().AlignRight().Text(docLabel)
                        .SemiBold().FontSize(16).FontColor(primary);

                    col.Item().AlignRight().Text(text =>
                    {
                        text.Span("Invoice No: ").SemiBold();
                        text.Span(_invoice.InvoiceNumber ?? "-");
                    });

                    col.Item().AlignRight().Text(text =>
                    {
                        text.Span("Date: ").SemiBold();
                        text.Span(_invoice.InvoiceDate.ToString("dd-MM-yyyy"));
                    });

                    if (_invoice.DocumentType == DocumentType.Quotation &&
                        _invoice.DueDate != null)
                    {
                        col.Item().AlignRight().Text(text =>
                        {
                            text.Span("Valid Until: ").SemiBold();
                            text.Span(_invoice.DueDate.Value.ToString("dd-MM-yyyy"));
                        });
                    }

                    if (!string.IsNullOrWhiteSpace(_invoice.ReferenceNumber))
                    {
                        col.Item().AlignRight().Text(text =>
                        {
                            text.Span("Reference: ").SemiBold();
                            text.Span(_invoice.ReferenceNumber);
                        });
                    }
                });
            });
        }

        // =====================================================================
        // CONTENT
        // =====================================================================
        void ComposeContent(IContainer container)
        {
            container.PaddingTop(10).Column(column =>
            {
                column.Spacing(10);

                column.Item().Element(ComposeBillShipSection);

                column.Item().Element(ComposeItemsTable);

                column.Item().Element(ComposeTotalsAndPaymentInfo);
            });
        }

        //void ComposeBillToSection(IContainer container)
        //{
        //    container.Row(row =>
        //    {
        //        row.RelativeItem().Column(col =>
        //        {
        //            col.Item().Text("Bill To").SemiBold().FontSize(11);
        //            col.Item().Text(_invoice.Customer?.CustomerName ?? "-");

        //            if (!string.IsNullOrWhiteSpace(_invoice.Customer?.BillingAddressLine1) ||
        //                !string.IsNullOrWhiteSpace(_invoice.Customer?.BillingAddressLine2))
        //            {
        //                col.Item().Text(
        //                    $"{_invoice.Customer?.BillingAddressLine1} {_invoice.Customer?.BillingAddressLine2}"
        //                );
        //            }

        //            if (!string.IsNullOrWhiteSpace(_invoice.Customer?.BillingCity))
        //                col.Item().Text(_invoice.Customer.BillingCity);

        //            if (!string.IsNullOrWhiteSpace(_invoice.Customer?.GSTNumber))
        //                col.Item().Text($"GSTIN: {_invoice.Customer.GSTNumber}");

        //        });

        //        row.RelativeItem().Column(col =>
        //        {
        //            col.Item().Text("Ship To").SemiBold().FontSize(11);

        //            // If you don’t store shipping separately, you can repeat billing
        //            col.Item().Text(_invoice.Customer?.CustomerName ?? _invoice.Customer?.CustomerName ?? "-");

        //            if (!string.IsNullOrWhiteSpace(_invoice.Customer?.ShippingAddressLine1) ||
        //                !string.IsNullOrWhiteSpace(_invoice.Customer?.ShippingAddressLine2))
        //            {
        //                col.Item().Text(
        //                    $"{_invoice.Customer?.ShippingAddressLine1} {_invoice.Customer?.ShippingAddressLine2}"
        //                );
        //            }

        //            if (!string.IsNullOrWhiteSpace(_invoice.Customer?.ShippingCity))
        //                col.Item().Text(_invoice.Customer.ShippingCity);

        //            if (!string.IsNullOrWhiteSpace(_invoice.PlaceOfSupply))
        //                col.Item().Text($"Place of Supply: {_invoice.PlaceOfSupply}");
        //        });
        //    });
        //}
        void ComposeBillShipSection(IContainer container)
        {
            container.Row(row =>
            {
                // ---------------------------
                // BILL TO
                // ---------------------------
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text("BILL TO").Bold().FontSize(11).Underline();

                    col.Item().PaddingBottom(3).Text(_invoice.Customer?.CustomerName ?? "-");

                    // Company Name (optional)
                    if (!string.IsNullOrWhiteSpace(_invoice.Customer?.CompanyName))
                        col.Item().Text(_invoice.Customer.CompanyName);

                    // Billing Address
                    if (!string.IsNullOrWhiteSpace(_invoice.Customer?.BillingAddressLine1))
                        col.Item().Text(_invoice.Customer.BillingAddressLine1);

                    if (!string.IsNullOrWhiteSpace(_invoice.Customer?.BillingAddressLine2))
                        col.Item().Text(_invoice.Customer.BillingAddressLine2);

                    // City, State PIN
                    if (!string.IsNullOrWhiteSpace(_invoice.Customer?.BillingCity) ||
                        !string.IsNullOrWhiteSpace(_invoice.Customer?.BillingState))
                    {
                        col.Item().Text(
                            $"{_invoice.Customer?.BillingCity}, {_invoice.Customer?.BillingState} {_invoice.Customer?.BillingPincode}"
                        );
                    }

                    // GST
                    if (!string.IsNullOrWhiteSpace(_invoice.Customer?.GSTNumber))
                        col.Item().PaddingTop(2).Text($"GSTIN: {_invoice.Customer.GSTNumber}");
                });

                // ---------------------------
                // SHIP TO
                // ---------------------------
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text("SHIP TO").Bold().FontSize(11).Underline();

                    var shipName = _invoice.Customer?.CustomerName ?? "-";

                    col.Item().PaddingBottom(3).Text(shipName);

                    // If shipping same as billing:
                    if (_invoice.Customer?.ShippingSame == true)
                    {
                        // Same as billing
                        if (!string.IsNullOrWhiteSpace(_invoice.Customer?.BillingAddressLine1))
                            col.Item().Text(_invoice.Customer.BillingAddressLine1);

                        if (!string.IsNullOrWhiteSpace(_invoice.Customer?.BillingAddressLine2))
                            col.Item().Text(_invoice.Customer.BillingAddressLine2);

                        col.Item().Text(
                            $"{_invoice.Customer?.BillingCity}, {_invoice.Customer?.BillingState} {_invoice.Customer?.BillingPincode}"
                        );
                    }
                    else
                    {
                        // Use shipping fields
                        if (!string.IsNullOrWhiteSpace(_invoice.Customer?.ShippingAddressLine1))
                            col.Item().Text(_invoice.Customer.ShippingAddressLine1);

                        if (!string.IsNullOrWhiteSpace(_invoice.Customer?.ShippingAddressLine2))
                            col.Item().Text(_invoice.Customer.ShippingAddressLine2);

                        col.Item().Text(
                            $"{_invoice.Customer?.ShippingCity}, {_invoice.Customer?.ShippingState} {_invoice.Customer?.ShippingPincode}"
                        );
                    }

                    // Optional: place of supply
                    if (!string.IsNullOrWhiteSpace(_invoice.PlaceOfSupply))
                        col.Item().PaddingTop(2).Text($"Place of Supply: {_invoice.PlaceOfSupply}");
                });
            });
        }

        void ComposeItemsTable(IContainer container)
        {
            var culture = CultureInfo.GetCultureInfo("en-IN");

            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3);  // Item
                    columns.RelativeColumn(1);  // HSN
                    columns.RelativeColumn(1);  // Qty
                    columns.RelativeColumn(1);  // Rate
                    columns.RelativeColumn(1);  // Disc%
                    columns.RelativeColumn(1);  // GST%
                    columns.RelativeColumn(1.2f);  // Amount
                });

                // Header
                table.Header(header =>
                {
                    header.Cell().Element(CellHeader).Text("Item");
                    header.Cell().Element(CellHeader).Text("HSN");
                    header.Cell().Element(CellHeader).AlignRight().Text("Qty");
                    header.Cell().Element(CellHeader).AlignRight().Text("Rate");
                    header.Cell().Element(CellHeader).AlignRight().Text("Disc%");
                    header.Cell().Element(CellHeader).AlignRight().Text("GST%");
                    header.Cell().Element(CellHeader).AlignRight().Text("Amount");
                });

                foreach (var item in _invoice.Items)
                {
                    table.Cell().Element(CellBody).Text(item.ItemName);
                    table.Cell().Element(CellBody).Text(item.HsnCode ?? "");
                    table.Cell().Element(CellBody).AlignRight().Text(item.Quantity.ToString("0.##"));
                    table.Cell().Element(CellBody).AlignRight().Text(item.UnitPrice.ToString("0.00"));
                    table.Cell().Element(CellBody).AlignRight().Text(item.DiscountPercent.ToString("0.##"));
                    table.Cell().Element(CellBody).AlignRight().Text(item.GstRate.ToString("0.##"));
                    table.Cell().Element(CellBody).AlignRight().Text(item.LineTotal.ToString("0.00", culture));
                }

                static IContainer CellHeader(IContainer container) =>
                    container.DefaultTextStyle(x => x.SemiBold())
                             .PaddingVertical(4)
                             .PaddingHorizontal(5)
                             .Background(Colors.Grey.Lighten3)
                             .BorderBottom(1)
                             .BorderColor(Colors.Grey.Lighten2);

                static IContainer CellBody(IContainer container) =>
                    container.PaddingVertical(3).PaddingHorizontal(5);
            });
        }

        void ComposeTotalsAndPaymentInfo(IContainer container)
        {
            var culture = CultureInfo.GetCultureInfo("en-IN");

            container.Row(row =>
            {
                // LEFT: payment info + QR
                row.RelativeItem().Column(col =>
                {
                    col.Spacing(6);

                    col.Item().Text("Payment Details").SemiBold().FontSize(11);

                    if (!string.IsNullOrWhiteSpace(_settings.BankName))
                        col.Item().Text($"Bank: {_settings.BankName}");

                    if (!string.IsNullOrWhiteSpace(_settings.AccountHolderName))
                        col.Item().Text($"Account Name: {_settings.AccountHolderName}");

                    if (!string.IsNullOrWhiteSpace(_settings.AccountNumber))
                        col.Item().Text($"Account No: {_settings.AccountNumber}");

                    if (!string.IsNullOrWhiteSpace(_settings.Ifsc))
                        col.Item().Text($"IFSC: {_settings.Ifsc}");

                    if (!string.IsNullOrWhiteSpace(_settings.UpiId))
                        col.Item().Text($"UPI ID: {_settings.UpiId}");

                    if (_qrBytes != null && _qrBytes.Length > 0)
                    {
                        col.Item().PaddingTop(5).Row(r =>
                        {
                            r.ConstantItem(80).Height(80).Image(_qrBytes);
                            r.RelativeItem().AlignMiddle().Text("Scan to Pay").Italic();
                        });
                    }
                });

                // RIGHT: totals
                row.ConstantItem(230).Column(col =>
                {
                    decimal subTotal = _invoice.SubTotal;
                    decimal tax = _invoice.TotalTax;
                    decimal transport = _invoice.TransportCharges;
                    decimal loading = _invoice.LoadingCharges;
                    decimal grand = _invoice.TotalAmount;

                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(c =>
                        {
                            c.RelativeColumn(1);
                            c.RelativeColumn(1);
                        });

                        void AddRow(string label, decimal value, bool bold = false)
                        {
                            table.Cell().Element(t =>
                                t.PaddingVertical(2)
                                 .AlignLeft()
                                 .DefaultTextStyle(x => { if (bold) x = x.SemiBold(); return x; })
                            ).Text(label);

                            table.Cell().Element(t =>
                                t.PaddingVertical(2)
                                 .AlignRight()
                                 .DefaultTextStyle(x => { if (bold) x = x.SemiBold(); return x; })
                            ).Text(value.ToString("0.00", culture));
                        }

                        AddRow("Sub Total", subTotal);
                        AddRow("Tax (GST)", tax);
                        AddRow("Transport Charges", transport);
                        AddRow("Loading Charges", loading);

                        table.Cell().ColumnSpan(2).PaddingVertical(2).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);

                        AddRow("Grand Total", grand, bold: true);
                    });

                    if (!string.IsNullOrWhiteSpace(_invoice.Notes))
                    {
                        col.Item().PaddingTop(8).Text("Notes:").SemiBold();
                        col.Item().Text(_invoice.Notes);
                    }
                });
            });
        }

        // =====================================================================
        // FOOTER
        // =====================================================================
        void ComposeFooter(IContainer container)
        {
            container.PaddingTop(8).Column(col =>
            {
                if (!string.IsNullOrWhiteSpace(_settings.TermsAndConditions))
                {
                    col.Item().Text("Terms & Conditions").SemiBold().FontSize(10);
                    col.Item().Text(_settings.TermsAndConditions).FontSize(9);
                }

                col.Item().PaddingTop(5).Row(row =>
                {
                    row.RelativeItem().Text(_settings.FooterText ??
                        "This is a computer generated document and does not require a signature.")
                        .FontSize(8).FontColor(Colors.Grey.Medium);

                    row.ConstantItem(60).AlignRight().Text(txt =>
                    {
                        txt.Span("Page ").FontSize(8);
                        txt.CurrentPageNumber();
                        txt.Span(" of ");
                        txt.TotalPages();
                    });
                });
            });
        }
    }
}


