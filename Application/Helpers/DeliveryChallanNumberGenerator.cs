using InvoicingAPI.Domain.Entities;
using InvoicingAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;



namespace InvoicingAPI.Application.Helpers
{
    public interface IDeliveryChallanNumberGenerator
    {
        Task<string> GenerateNextNumberAsync(Guid companyId);
    }
    public class DeliveryChallanNumberGenerator : IDeliveryChallanNumberGenerator
    {
        private readonly AppDbContext _context;

        public DeliveryChallanNumberGenerator(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateNextNumberAsync(Guid companyId)
        {
            const string documentType = "DeliveryChallan";
            const string defaultPrefix = "DC";
            const int defaultPadding = 5;

            using var tx = await _context.Database.BeginTransactionAsync();

            var counter = await _context.DeliveryChallanCounters
                .FirstOrDefaultAsync(x => x.CompanyId == companyId &&
                                          x.DocumentType == documentType);

            if (counter == null)
            {
                counter = new DeliveryChallanCounters
                {
                    Id = Guid.NewGuid(),
                    CompanyId = companyId,
                    DocumentType = documentType,
                    Prefix = defaultPrefix,
                    Padding = defaultPadding,
                    CurrentNumber = 0
                };

                _context.DeliveryChallanCounters.Add(counter);
            }

            counter.CurrentNumber++;
            await _context.SaveChangesAsync();
            await tx.CommitAsync();

            var number = counter.CurrentNumber.ToString().PadLeft(counter.Padding, '0');

            return $"{counter.Prefix}-{number}";
        }
    }

}
