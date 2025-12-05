using System;
using System.Collections.Generic;

namespace InvoicingAPI.Domain.Entities
{
    public enum DocumentType
    {
        Quotation = 1,
        Invoice = 2
    }

    // Lifecycle / document status
    public enum DocumentStatus
    {
        Draft = 1,
        Final = 2,
        Cancelled = 3,

        QuotationSent = 10,
        QuotationAccepted = 11,
        QuotationRejected = 12,
        QuotationConverted = 13
    }

    // Payment status (for invoices only)
    public enum PaymentStatus
    {
        Unpaid = 1,
        PartiallyPaid = 2,
        Paid = 3,
        Overdue = 4
    }

    public class InvoiceDocument
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CreatedByUserId { get; set; }

        // Type + Status
        public DocumentType DocumentType { get; set; }
        public DocumentStatus Status { get; set; }          // document lifecycle
        public PaymentStatus? PaymentStatus { get; set; }   // invoice only

        // Numbering
        public string InvoiceNumber { get; set; }

        // Dates
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }

        // PO / Reference
        public string ReferenceNumber { get; set; }

        // GST details
        public string PlaceOfSupply { get; set; }
        public bool IsGstInclusive { get; set; }

        // Display fields
        public string Notes { get; set; }
        public string TermsAndConditions { get; set; }

        // Totals
        public decimal SubTotal { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal? RoundOff { get; set; }

        // Quotation → Invoice
        public Guid? ConvertedFromQuotationId { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation
        public Company Company { get; set; }
        public Customer Customer { get; set; }
        public User CreatedByUser { get; set; }
        public decimal TransportCharges { get; set; }
        public decimal LoadingCharges { get; set; }

        public ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    }
}
