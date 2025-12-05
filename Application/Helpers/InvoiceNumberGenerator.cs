using InvoicingAPI.Domain.Entities;
using InvoicingAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InvoicingAPI.Application.Helpers
{
    public interface IInvoiceNumberGenerator
    {
        Task<string> PreviewAsync(Guid companyId);
        Task<string> GenerateFinalAsync(Guid companyId);
    }

    public class InvoiceNumberGenerator : IInvoiceNumberGenerator
    {
        private readonly AppDbContext _context;

        public InvoiceNumberGenerator(AppDbContext context)
        {
            _context = context;
        }

        private async Task<InvoiceNumberSettings> GetSettings(Guid companyId)
        {
            var settings = await _context.InvoiceNumberSettings
                .FirstOrDefaultAsync(x => x.CompanyId == companyId && x.IsDefault);

            if (settings == null)
            {
                settings = new InvoiceNumberSettings
                {
                    Id = Guid.NewGuid(),
                    CompanyId = companyId,
                    Template = "{PREFIX}{SEQ}{SUFFIX}",
                    Prefix = "",
                    Suffix = "",
                    Padding = 3,
                    ResetFrequency = NumberResetFrequency.Never,
                };

                _context.InvoiceNumberSettings.Add(settings);
                await _context.SaveChangesAsync();
            }

            return settings;
        }

        private string Format(InvoiceNumberSettings s, int seq)
        {
            string padded = seq.ToString().PadLeft(s.Padding, '0');
            return s.Template
                    .Replace("{PREFIX}", s.Prefix ?? "")
                    .Replace("{SEQ}", padded)
                    .Replace("{SUFFIX}", s.Suffix ?? "")
                    .Replace("{YYYY}", DateTime.UtcNow.ToString("yyyy"))
                    .Replace("{MM}", DateTime.UtcNow.ToString("MM"));
        }

        // --------------------------
        // PREVIEW (NO SAVE, NO INCREMENT)
        // --------------------------
        public async Task<string> PreviewAsync(Guid companyId)
        {
            var settings = await GetSettings(companyId);
            int previewSeq = settings.CurrentNumber + 1;

            return Format(settings, previewSeq);
        }

        // --------------------------
        // FINAL NUMBER (INCREMENT + SAVE)
        // --------------------------
        public async Task<string> GenerateFinalAsync(Guid companyId)
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            var settings = await GetSettings(companyId);

            var now = DateTime.UtcNow;

            // Reset rules
            bool reset =
                (settings.ResetFrequency == NumberResetFrequency.Yearly && settings.CurrentYear != now.Year) ||
                (settings.ResetFrequency == NumberResetFrequency.Monthly &&
                 (settings.CurrentYear != now.Year || settings.CurrentMonth != now.Month));

            if (reset)
            {
                settings.CurrentNumber = 0;
                settings.CurrentYear = now.Year;
                settings.CurrentMonth = now.Month;
            }

            settings.CurrentNumber += 1;
            await _context.SaveChangesAsync();

            string final = Format(settings, settings.CurrentNumber);

            await tx.CommitAsync();
            return final;
        }
    }
}
