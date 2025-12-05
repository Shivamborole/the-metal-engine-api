

using InvoicingAPI.Domain.Entities;
using InvoicingAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace InvoicingAPI.Application.Helpers
{
    public interface IRejectionNoteNumberGenerator
    {
        Task<string> GenerateNextNumberAsync(Guid companyId);
    }
    public class RejectionNoteNumberGenerator : IRejectionNoteNumberGenerator
    {
        private readonly AppDbContext _context;

        public RejectionNoteNumberGenerator(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateNextNumberAsync(Guid companyId)
        {
            const string documentType = "RejectionNote";
            const string defaultPrefix = "RN";
            const int defaultPadding = 5;

            using var tx = await _context.Database.BeginTransactionAsync();

            var counter = await _context.RejectionNoteCounters
                .FirstOrDefaultAsync(x => x.CompanyId == companyId &&
                                          x.DocumentType == documentType);

            if (counter == null)
            {
                counter = new RejectionNoteCounters
                {
                    Id = Guid.NewGuid(),
                    CompanyId = companyId,
                    DocumentType = documentType,
                    Prefix = defaultPrefix,
                    Padding = defaultPadding,
                    CurrentNumber = 0
                };

                _context.RejectionNoteCounters.Add(counter);
            }

            counter.CurrentNumber++;
            await _context.SaveChangesAsync();
            await tx.CommitAsync();

            var num = counter.CurrentNumber.ToString().PadLeft(counter.Padding, '0');

            return $"{counter.Prefix}-{num}";
        }
    }

}
