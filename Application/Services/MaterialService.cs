using InvoicingAPI.Domain.Entities;
using InvoicingAPI.Infrastructure.Persistence;
using InvoicingAPI.Services;

public class MaterialService
{
    private readonly AppDbContext _context;
    private readonly StockService _stockService;

    public MaterialService(AppDbContext context, StockService stockService)
    {
        _context = context;
        _stockService = stockService;
    }

    public async Task<bool> AddPurchaseAsync(MaterialPurchase purchase)
    {
        purchase.Id = Guid.NewGuid();
        purchase.CreatedAt = DateTime.UtcNow;

        using var trx = await _context.Database.BeginTransactionAsync();

        try
        {
            // 1) Save Purchase Entry
            await _context.MaterialPurchases.AddAsync(purchase);
            await _context.SaveChangesAsync();   // IMPORTANT: generate ID

            // 2) Insert Stock-IN transaction
            await _stockService.AddMaterialStockAsync(
     materialId: purchase.MaterialId,
     quantity: purchase.Quantity,
     unitCost: purchase.UnitPrice,
     transactionType: "Purchase",
     referenceNo: purchase.ReferenceNumber,
     sourceId: purchase.Id
 );


            await trx.CommitAsync();
            return true;
        }
        catch
        {
            await trx.RollbackAsync();
            throw;
        }
    }


}
