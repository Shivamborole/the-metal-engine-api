using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using InvoicingAPI.Application.DTO;
using InvoicingAPI.Application.Helpers;
using InvoicingAPI.Domain.Entities;
using InvoicingAPI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using QuestPDF.Fluent;

namespace InvoicingAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IInvoiceNumberGenerator _numberGen;

        public InvoicesController(AppDbContext context, IInvoiceNumberGenerator numberGen)
        {
            _context = context;
            _numberGen = numberGen;
        }

        // ============================================================================
        // GET INVOICES + QUOTATIONS LIST
        // ============================================================================
        [HttpGet]
        public async Task<IActionResult> GetInvoices([FromQuery] Guid companyId)
        {
            if (companyId == Guid.Empty)
                return BadRequest("Company ID is required.");

            var data = await _context.InvoiceDocuments
                .Include(x => x.Customer)
                .Where(x => x.CompanyId == companyId)
                .OrderByDescending(x => x.InvoiceDate)
                .Select(x => new InvoiceListDto
                {
                    Id = x.Id,
                    InvoiceNumber = x.InvoiceNumber,
                    InvoiceDate = x.InvoiceDate,
                    DocumentType = x.DocumentType,
                    CustomerName = x.Customer.CustomerName,
                    TotalAmount = x.TotalAmount,
                    PaymentStatus = x.PaymentStatus,
                    Status = x.Status
                })
                .ToListAsync();

            return Ok(data);
        }

        // ============================================================================
        // PREVIEW NEXT INVOICE NUMBER (NO COUNTER INCREMENT)
        // ============================================================================
        [HttpGet("preview-number")]
        public async Task<IActionResult> PreviewNumber([FromQuery] Guid companyId)
        {
            if (companyId == Guid.Empty)
                return BadRequest("Company ID is required.");

            var preview = await _numberGen.PreviewAsync(companyId);
            return Ok(new { invoiceNumber = preview });
        }

        // ============================================================================
        // GET SINGLE DOCUMENT
        // ============================================================================
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetInvoice(Guid id)
        {
            var invoice = await _context.InvoiceDocuments
                .Include(x => x.Customer)
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (invoice == null)
                return NotFound("Document not found.");

            return Ok(invoice);
        }

        // ============================================================================
        // CREATE DOCUMENT (INVOICE or QUOTATION)
        // ============================================================================
        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            if (dto.Items == null || dto.Items.Count == 0)
                return BadRequest("Document must contain at least one item.");

            var userIdString =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrWhiteSpace(userIdString))
                return Unauthorized("User id missing in access token");

            // Generate Invoice #
            string finalNumber = await _numberGen.GenerateFinalAsync(dto.CompanyId);

            var doc = new InvoiceDocument
            {
                Id = Guid.NewGuid(),
                CompanyId = dto.CompanyId,
                CustomerId = dto.CustomerId,
                CreatedByUserId = Guid.Parse(userIdString),

                DocumentType = dto.DocumentType,
                InvoiceNumber = finalNumber,
                InvoiceDate = dto.InvoiceDate,
                DueDate = dto.DueDate,
                ReferenceNumber = dto.ReferenceNumber,
                PlaceOfSupply = dto.PlaceOfSupply,
                Notes = dto.Notes,
                TermsAndConditions = dto.TermsAndConditions,
                IsGstInclusive = dto.IsGstInclusive,
                CreatedAt = DateTime.UtcNow,           
                TransportCharges = dto.TransportCharges,
                LoadingCharges = dto.LoadingCharges,
            
            };

            // Set Status based on type
            if (dto.DocumentType == DocumentType.Invoice)
            {
                doc.Status = DocumentStatus.Final;
                doc.PaymentStatus = PaymentStatus.Unpaid;
            }
            else
            {
                doc.Status = DocumentStatus.QuotationSent;
                doc.PaymentStatus = null;
            }

            decimal subTotal = 0;
            decimal totalTax = 0;

            foreach (var itemDto in dto.Items)
            {
                decimal baseLine = itemDto.Quantity * itemDto.UnitPrice;
                decimal discount = baseLine * (itemDto.DiscountPercent / 100);
                decimal taxable = baseLine - discount;
                decimal gst = taxable * (itemDto.GstRate / 100);
                decimal lineTotal = taxable + gst;

                doc.Items.Add(new InvoiceItem
                {
                    Id = Guid.NewGuid(),
                    InvoiceDocumentId = doc.Id,
                    ProductId = itemDto.ProductId,
                    ItemName = itemDto.ItemName,
                    HsnCode = itemDto.HsnCode,
                    Unit = itemDto.Unit,
                    Quantity = itemDto.Quantity,
                    UnitPrice = itemDto.UnitPrice,
                    DiscountPercent = itemDto.DiscountPercent,
                    DiscountAmount = discount,
                    GstRate = itemDto.GstRate,
                    GstAmount = gst,
                    LineTotal = lineTotal,
                    SaveAsProduct = itemDto.SaveAsProduct
                });

                subTotal += taxable;
                totalTax += gst;

                // Auto-add product
                if (itemDto.ProductId == null && itemDto.SaveAsProduct)
                {
                    _context.Products.Add(new Product
                    {
                        Id = Guid.NewGuid(),
                        CompanyId = dto.CompanyId,
                        Name = itemDto.ItemName,
                        HsnOrSac = itemDto.HsnCode,
                        UnitPrice = itemDto.UnitPrice,
                        GstRate = itemDto.GstRate,
                        IsActive = true
                    });
                }
            }

            doc.SubTotal = subTotal;
            doc.TotalTax = totalTax;
            //doc.TotalAmount = Math.Round(subTotal + totalTax);
            doc.TotalAmount = Math.Round(subTotal + totalTax + dto.TransportCharges + dto.LoadingCharges);

            _context.InvoiceDocuments.Add(doc);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                invoiceId = doc.Id,
                invoiceNumber = doc.InvoiceNumber,
                message = "Document created successfully"
            });
        }

        // ============================================================================
        // UPDATE PAYMENT STATUS (Invoice Only)
        // ============================================================================
        [HttpPut("{id:guid}/payment-status")]
        public async Task<IActionResult> UpdatePaymentStatus(Guid id, [FromBody] PaymentStatus status)
        {
            var doc = await _context.InvoiceDocuments.FindAsync(id);
            if (doc == null)
                return NotFound("Document not found");

            if (doc.DocumentType != DocumentType.Invoice)
                return BadRequest("Only invoices can have payment statuses");

            doc.PaymentStatus = status;
            await _context.SaveChangesAsync();

            return Ok("Payment updated");
        }

        // ============================================================================
        // CONVERT QUOTATION → INVOICE
        // ============================================================================
        [HttpPut("{id:guid}/convert")]
        public async Task<IActionResult> ConvertQuotation(Guid id)
        {
            var quotation = await _context.InvoiceDocuments
                .FirstOrDefaultAsync(x => x.Id == id && x.DocumentType == DocumentType.Quotation);

            if (quotation == null)
                return NotFound("Quotation not found.");

            // Change type
            quotation.DocumentType = DocumentType.Invoice;
            quotation.Status = DocumentStatus.Final;
            quotation.PaymentStatus = PaymentStatus.Unpaid;

            // Generate new Invoice Number
            quotation.InvoiceNumber = await _numberGen.GenerateFinalAsync(quotation.CompanyId);

            await _context.SaveChangesAsync();

            return Ok("Quotation converted to Invoice");
        }

        // ============================================================================
        // GET PDF (INVOICE OR QUOTATION)
        // ============================================================================
        [AllowAnonymous]  // or [Authorize] if frontend sends token correctly
        [HttpGet("{id:guid}/pdf")]
        public async Task<IActionResult> GetInvoicePdf(Guid id)
        {
            var invoice = await _context.InvoiceDocuments
                .Include(x => x.Customer)
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (invoice == null)
                return NotFound("Invoice / Quotation not found");

            // TODO: load from DB table later. For now, hard-code or map from Company.
            var pdfSettings = await _context.Set<InvoicePdfSettings>()
                .FirstOrDefaultAsync(s => s.CompanyId == invoice.CompanyId);

            if (pdfSettings == null)
            {
                pdfSettings = new InvoicePdfSettings
                {
                    CompanyId = invoice.CompanyId,
                    CompanyDisplayName = "TestCompany",
                    CompanyAddressLine1 = "Address line 1",
                    CompanyAddressLine2 = "Pune, Maharashtra",
                    CompanyGstin = "22AAAAA0000A1Z5",
                    CompanyPhone = "+91-9999999999",
                    CompanyEmail = "billing@testcompany.com",
                    BankName = "HDFC Bank",
                    AccountHolderName = "TestCompany Pvt Ltd",
                    AccountNumber = "1234567890",
                    Ifsc = "HDFC0001234",
                    UpiId = "testcompany@upi",
                    UpiPayeeName = "TestCompany",
                    TermsAndConditions = "Goods once sold will not be taken back.",
                    FooterText = "Thank you for your business."
                };
            }

            // Generate UPI QR if UPI is configured
            byte[]? qrPngBytes = null;
            if (!string.IsNullOrWhiteSpace(pdfSettings.UpiId))
            {
                var upiString = $"upi://pay?pa={pdfSettings.UpiId}&pn={Uri.EscapeDataString(pdfSettings.UpiPayeeName ?? pdfSettings.CompanyDisplayName ?? "Vendor")}&cu=INR";

                using var generator = new QRCodeGenerator();
                using var data = generator.CreateQrCode(upiString, QRCodeGenerator.ECCLevel.Q);
                var pngQr = new PngByteQRCode(data);
                qrPngBytes = pngQr.GetGraphic(20);   // 20 = pixels per module
            }

            // Logo: later you can load from blob / file / URL ⇒ byte[]
            byte[]? logoBytes = null;

            var document = new InvoicePdfDocument(invoice, pdfSettings, qrPngBytes, logoBytes);
            var pdfBytes = document.GeneratePdf();   // QuestPDF extension

            var fileName = $"{invoice.InvoiceNumber}.pdf";
            return File(pdfBytes, "application/pdf", fileName);
        }
    }
}
