using InvoicingAPI.Domain.Entities;
using InvoicingAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InvoicingAPI.Services
{
    public class StockService
    {
        private readonly AppDbContext _context;

        public StockService(AppDbContext context)
        {
            _context = context;
        }

        // MATERIAL INWARD (PURCHASE)
        public async Task<bool> AddMaterialStockAsync(Guid materialId, decimal quantity, decimal unitCost,
                                                      string transactionType, string referenceNo, Guid? sourceId)
        {
            var material = await _context.Materials.FindAsync(materialId);
            if (material == null)
                throw new Exception("Material not found.");

            // Update current stock
            material.CurrentStock += quantity;

            // Create transaction log
            var trx = new StockTransaction
            {
                Id = Guid.NewGuid(),
                CompanyId = material.CompanyId,
                MaterialId = materialId,

                QuantityChange = quantity,                   // +ve
                TransactionType = transactionType,           // "Purchase"
                SourceReference = referenceNo,
                SourceId = sourceId,

                Notes = $"Purchased {quantity} units at {unitCost} each",
                CreatedAt = DateTime.UtcNow
            };

            await _context.StockTransactions.AddAsync(trx);
            await _context.SaveChangesAsync();
            return true;
        }


        // MATERIAL OUTWARD (CONSUMPTION VIA INVOICE OR PRODUCTION)
        public async Task<bool> ConsumeMaterialAsync(Guid materialId, decimal quantity,
                                                     string transactionType, string referenceNo, Guid? sourceId)
        {
            var material = await _context.Materials.FindAsync(materialId);
            if (material == null)
                throw new Exception("Material not found.");

            // prevent negative stock
            if (material.CurrentStock < quantity)
                throw new Exception("Insufficient stock.");

            material.CurrentStock -= quantity;

            var trx = new StockTransaction
            {
                Id = Guid.NewGuid(),
                CompanyId = material.CompanyId,
                MaterialId = materialId,

                QuantityChange = -Math.Abs(quantity),         // -ve
                TransactionType = transactionType,            // "Invoice", "Production"
                SourceReference = referenceNo,
                SourceId = sourceId,

                Notes = $"Consumed {quantity} units",
                CreatedAt = DateTime.UtcNow
            };

            await _context.StockTransactions.AddAsync(trx);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ConsumeProductMaterialAsync(Guid productId, decimal productQuantity, string referenceNo, Guid? sourceId)
        {
            // Load product + its BOM
            var product = await _context.Products
                .Include(p => p.BOM)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
                throw new Exception("Product not found.");

            if (product.BOM == null || !product.BOM.Any())
                return true; // No BOM means no material consumption

            foreach (var bom in product.BOM)
            {
                var materialQtyNeeded = bom.MaterialQuantity * productQuantity;

                await ConsumeMaterialAsync(
                    materialId: bom.MaterialId,
                    quantity: materialQtyNeeded,
                    transactionType: "Invoice",
                    referenceNo: referenceNo,
                    sourceId: sourceId
                );
            }

            return true;
        }

    }
}
