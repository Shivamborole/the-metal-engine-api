using System;
using System.Linq;
using System.Threading.Tasks;
using InvoicingAPI.Application.DTO;
using InvoicingAPI.Application.Helpers;
using InvoicingAPI.Domain.Entities;
using InvoicingAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InvoicingAPI.Application.Services
{
    public interface IDeliveryService
    {
        Task<DeliveryChallan> CreateDeliveryChallanAsync(CreateDeliveryChallanRequest request);
        Task<InvoiceDeliverySummaryDto> GetInvoiceDeliverySummaryAsync(Guid invoiceDocumentId);
    }

    public class DeliveryService : IDeliveryService
    {
        private readonly AppDbContext _context;
        private readonly IDeliveryChallanNumberGenerator _numberGenerator;

        public DeliveryService(
            AppDbContext context,
            IDeliveryChallanNumberGenerator numberGenerator)
        {
            _context = context;
            _numberGenerator = numberGenerator;
        }

        // ---------------------------------------------------------
        // SUMMARY for INVOICE DELIVERY
        // ---------------------------------------------------------
        public async Task<InvoiceDeliverySummaryDto> GetInvoiceDeliverySummaryAsync(Guid invoiceDocumentId)
        {
            var invoice = await _context.InvoiceDocuments
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == invoiceDocumentId);

            if (invoice == null)
                throw new InvalidOperationException("Invoice not found.");

            var challanItems = await _context.DeliveryChallanItems
                .Include(x => x.DeliveryChallan)
                .Where(x => x.DeliveryChallan.InvoiceDocumentId == invoiceDocumentId &&
                            x.DeliveryChallan.Status != DeliveryChallanStatus.Cancelled)
                .ToListAsync();

            var rejectionItems = await _context.RejectionNoteItems
                .Include(x => x.RejectionNote)
                .Where(x => x.RejectionNote.InvoiceDocumentId == invoiceDocumentId)
                .ToListAsync();

            var summary = new InvoiceDeliverySummaryDto
            {
                InvoiceDocumentId = invoice.Id,
                TotalInvoiceQty = invoice.Items.Sum(x => x.Quantity)
            };

            foreach (var item in invoice.Items)
            {
                var delivered = challanItems
                    .Where(c => c.InvoiceItemId == item.Id)
                    .Sum(c => c.Quantity);

                var rejected = rejectionItems
                    .Where(r => r.InvoiceItemId == item.Id)
                    .Sum(r => r.RejectedQty);

                var effective = delivered - rejected;
                if (effective < 0) effective = 0;

                var pending = item.Quantity - effective;
                if (pending < 0) pending = 0;

                var replacedQty = challanItems
                    .Where(c => c.InvoiceItemId == item.Id &&
                                c.DeliveryChallan.Type == DeliveryChallanType.Replacement)
                    .Sum(c => c.Quantity);

                var remainingToReplace = rejected - replacedQty;
                if (remainingToReplace < 0) remainingToReplace = 0;

                summary.Items.Add(new InvoiceDeliveryItemSummaryDto
                {
                    InvoiceItemId = item.Id,
                    ItemName = item.ItemName,
                    InvoiceQty = item.Quantity,
                    DeliveredQty = delivered,
                    RejectedQty = rejected,
                    EffectiveDeliveredQty = effective,
                    PendingQty = pending,
                    RemainingRejectionToReplace = remainingToReplace
                });

                summary.TotalDeliveredQty += delivered;
                summary.TotalRejectedQty += rejected;
                summary.TotalEffectiveDeliveredQty += effective;
            }

            summary.TotalPendingQty = summary.TotalInvoiceQty - summary.TotalEffectiveDeliveredQty;
            if (summary.TotalPendingQty < 0) summary.TotalPendingQty = 0;

            return summary;
        }

        // ---------------------------------------------------------
        // CREATE DELIVERY CHALLAN
        // ---------------------------------------------------------
        public async Task<DeliveryChallan> CreateDeliveryChallanAsync(CreateDeliveryChallanRequest request)
        {
            if (request.Items == null || !request.Items.Any())
                throw new InvalidOperationException("At least one item required.");

            using var tx = await _context.Database.BeginTransactionAsync();

            var invoice = await _context.InvoiceDocuments
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == request.InvoiceDocumentId)
                ?? throw new InvalidOperationException("Invoice not found.");

            if (invoice.Status == DocumentStatus.Cancelled)
                throw new InvalidOperationException("Cannot create challan for cancelled invoice.");

            var invoiceItemIds = request.Items.Select(x => x.InvoiceItemId).ToList();

            var invoiceItems = invoice.Items
                .Where(i => invoiceItemIds.Contains(i.Id))
                .ToList();

            if (invoiceItems.Count != invoiceItemIds.Count)
                throw new InvalidOperationException("Invalid InvoiceItemId in request.");

            var existingChallanItems = await _context.DeliveryChallanItems
                .Include(c => c.DeliveryChallan)
                .Where(c => c.DeliveryChallan.InvoiceDocumentId == invoice.Id &&
                            c.DeliveryChallan.Status != DeliveryChallanStatus.Cancelled)
                .ToListAsync();

            var rejectionItems = await _context.RejectionNoteItems
                .Include(r => r.RejectionNote)
                .Where(r => r.RejectionNote.InvoiceDocumentId == invoice.Id)
                .ToListAsync();

            // ----- VALIDATION -----
            foreach (var reqItem in request.Items)
            {
                var invItem = invoiceItems.First(i => i.Id == reqItem.InvoiceItemId);

                var delivered = existingChallanItems
                    .Where(c => c.InvoiceItemId == invItem.Id)
                    .Sum(c => c.Quantity);

                var rejected = rejectionItems
                    .Where(r => r.InvoiceItemId == invItem.Id)
                    .Sum(r => r.RejectedQty);

                var effective = delivered - rejected;
                if (effective < 0) effective = 0;

                var pending = invItem.Quantity - effective;
                if (pending < 0) pending = 0;

                var replaced = existingChallanItems
                    .Where(c => c.InvoiceItemId == invItem.Id &&
                                c.DeliveryChallan.Type == DeliveryChallanType.Replacement)
                    .Sum(c => c.Quantity);

                var remainingToReplace = rejected - replaced;
                if (remainingToReplace < 0) remainingToReplace = 0;

                if (request.Type == DeliveryChallanType.Normal)
                {
                    if (reqItem.Quantity > pending)
                        throw new InvalidOperationException(
                            $"Cannot deliver more than pending for {invItem.ItemName}. Pending: {pending}");
                }
                else
                {
                    if (reqItem.Quantity > remainingToReplace)
                        throw new InvalidOperationException(
                            $"Cannot replace more than rem. rejected for {invItem.ItemName}. Rem: {remainingToReplace}");
                }
            }

            // ----- CREATE CHALLAN -----
            var challan = new DeliveryChallan
            {
                Id = Guid.NewGuid(),
                CompanyId = request.CompanyId,
                InvoiceDocumentId = invoice.Id,
                CustomerId = request.CustomerId,
                ChallanDate = request.ChallanDate,
                Type = request.Type,
                Status = DeliveryChallanStatus.Final,
                VehicleNumber = request.VehicleNumber,
                TransporterName = request.TransporterName,
                Notes = request.Notes,
                CreatedByUserId = request.CreatedByUserId,
                CreatedAt = DateTime.UtcNow,
                ChallanNumber = await _numberGenerator.GenerateNextNumberAsync(request.CompanyId)
            };

            foreach (var itemReq in request.Items)
            {
                var invItem = invoiceItems.First(i => i.Id == itemReq.InvoiceItemId);

                challan.Items.Add(new DeliveryChallanItems
                {
                    Id = Guid.NewGuid(),
                    DeliveryChallanId = challan.Id,
                    InvoiceItemId = invItem.Id,
                    ProductId = invItem.ProductId,
                    ItemName = invItem.ItemName,
                    Unit = invItem.Unit,
                    Quantity = itemReq.Quantity,
                    Remarks = itemReq.Remarks
                });
            }

            _context.DeliveryChallan.Add(challan);
            await _context.SaveChangesAsync();
            await tx.CommitAsync();

            return challan;
        }
    }
}
