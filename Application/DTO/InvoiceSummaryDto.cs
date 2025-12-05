using InvoicingAPI.Domain.Entities;

namespace InvoicingAPI.Application.DTO
{
    public class InvoiceSummaryDto
    {
        public Guid Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DocumentType DocumentType { get; set; }

        // Normalized, human-readable status string for UI:
        // "paid", "unpaid", "overdue", "draft", "sent", etc.
        public string Status { get; set; }

        public DateTime InvoiceDate { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
