using InvoicingAPI.Domain.Entities;
using InvoicingAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InvoicingAPI.Services
{
    public class UnitConversionService
    {
        private readonly AppDbContext _context;

        public UnitConversionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> ConvertAsync(Guid companyId, Guid productId, decimal quantity)
        {
            var conv = await _context.UnitConversions
                .FirstOrDefaultAsync(x => x.CompanyId == companyId && x.ProductId == productId);

            if (conv == null)
                return quantity;  // No conversion → return same

            return quantity * conv.ConversionRate;
        }
    }
}
