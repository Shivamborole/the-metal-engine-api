using InvoicingAPI.Application.DTO;
using InvoicingAPI.Application.Helpers;
using InvoicingAPI.Domain.Entities;
using InvoicingAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InvoicingAPI.Application.Services
{

    public interface IRejectionNoteService
    {
        Task<RejectionNote> CreateRejectionNoteAsync(CreateRejectionNoteRequest request);
    }
    public class RejectionNoteService : IRejectionNoteService
    {
        private readonly AppDbContext _context;
        private readonly IRejectionNoteNumberGenerator _numberGenerator;

        public RejectionNoteService(
            AppDbContext context,
            IRejectionNoteNumberGenerator numberGenerator)
        {
            _context = context;
            _numberGenerator = numberGenerator;
        }

        public async Task<RejectionNote> CreateRejectionNoteAsync(CreateRejectionNoteRequest request)
        {
            if (request.Items == null || !request.Items.Any())
                throw new InvalidOperationException("At least one rejection item is required.");

            using var tx = await _context.Database.BeginTransactionAsync();

            var invoice = await _context.InvoiceDocuments
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == request.InvoiceDocumentId)
                ?? throw new InvalidOperationException("Invoice not found.");

            var challan = await _context.DeliveryChallan
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == request.DeliveryChallanId)
                ?? throw new InvalidOperationException("Delivery challan not found.");

            var deliveryItemIds = request.Items.Select(x => x.DeliveryChallanItemId).ToList();
            var challanItems = challan.Items.Where(i => deliveryItemIds.Contains(i.Id)).ToList();

            if (deliveryItemIds.Count != challanItems.Count)
                throw new InvalidOperationException("One or more delivery item IDs are invalid for this challan.");

            // Load existing rejections for validation
            var existingRejections = await _context.RejectionNoteItems
                .Include(r => r.RejectionNote)
                .Where(r => r.RejectionNote.DeliveryChallanId == challan.Id &&
                            deliveryItemIds.Contains(r.DeliveryChallanItemId))
                .ToListAsync();

            // VALIDATION LOOP
            foreach (var reqItem in request.Items)
            {
                var delItem = challanItems.First(x => x.Id == reqItem.DeliveryChallanItemId);

                if (reqItem.RejectedQty <= 0)
                    throw new InvalidOperationException("Rejected quantity must be greater than zero.");

                var alreadyRejected = existingRejections
                    .Where(r => r.DeliveryChallanItemId == delItem.Id)
                    .Sum(r => r.RejectedQty);

                var maxRejectable = delItem.Quantity - alreadyRejected;

                if (reqItem.RejectedQty > maxRejectable)
                    throw new InvalidOperationException(
                        $"Cannot reject more than available. Delivered: {delItem.Quantity}, Already Rejected: {alreadyRejected}, Max Rejectable: {maxRejectable}");
            }

            // CREATE REJECTION NOTE
            var rn = new RejectionNote
            {
                Id = Guid.NewGuid(),
                CompanyId = request.CompanyId,
                InvoiceDocumentId = invoice.Id,
                DeliveryChallanId = challan.Id,
                CustomerId = request.CustomerId,
                RejectionDate = request.RejectionDate,
                Reason = request.Reason,
                Notes = request.Notes,
                CreatedByUserId = request.CreatedByUserId,
                CreatedAt = DateTime.UtcNow,
                RejectionNumber = await _numberGenerator.GenerateNextNumberAsync(request.CompanyId)
            };

            // ADD ITEMS
            foreach (var reqItem in request.Items)
            {
                var delItem = challanItems.First(i => i.Id == reqItem.DeliveryChallanItemId);
                var invItem = invoice.Items.First(i => i.Id == delItem.InvoiceItemId);

                rn.Items.Add(new RejectionNoteItems
                {
                    Id = Guid.NewGuid(),
                    RejectionNoteId = rn.Id,
                    InvoiceItemId = invItem.Id,
                    DeliveryChallanItemId = delItem.Id,
                    ProductId = invItem.ProductId,
                    ItemName = invItem.ItemName,
                    Unit = invItem.Unit,
                    RejectedQty = reqItem.RejectedQty,
                    Reason = reqItem.Reason
                });
            }

            _context.RejectionNote.Add(rn);
            await _context.SaveChangesAsync();
            await tx.CommitAsync();

            return rn;
        }
    }
}
