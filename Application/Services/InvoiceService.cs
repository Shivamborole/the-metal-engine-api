using InvoicingAPI.Domain.Entities;
using InvoicingAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InvoicingAPI.Services
{
    public class InvoiceService
    {
        private readonly AppDbContext _context;
        private readonly StockService _stockService;

        public InvoiceService(AppDbContext context, StockService stockService)
        {
            _context = context;
            _stockService = stockService;
        }

        // ============================================
        // CREATE INVOICE + AUTO-CONSUME STOCK USING BOM
        // ============================================
        public async Task<bool> CreateInvoiceAsync(InvoiceDocument invoice)
        {
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice));

            // Save invoice first
            await _context.InvoiceDocuments.AddAsync(invoice);

            // For each item → reduce material stock
            foreach (var item in invoice.Items)
            {
                if (item.ProductId.HasValue)
                {
                    await _stockService.ConsumeProductMaterialAsync(
      productId: item.ProductId.Value,
      productQuantity: item.Quantity,
      referenceNo: invoice.InvoiceNumber,
      sourceId: invoice.Id
  );

                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        // ============================================
        // GET INVOICE
        // ============================================
        public async Task<InvoiceDocument?> GetInvoiceAsync(Guid id)
        {
            return await _context.InvoiceDocuments
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        // ============================================
        // LIST INVOICES FOR COMPANY
        // ============================================
        public async Task<List<InvoiceDocument>> GetInvoicesAsync(Guid companyId)
        {
            return await _context.InvoiceDocuments
                .Include(i => i.Customer)
                .Where(i => i.CompanyId == companyId)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }
    }
}
