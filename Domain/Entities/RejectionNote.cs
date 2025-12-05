using System;
using System.Collections.Generic;

namespace InvoicingAPI.Domain.Entities
{
    public class RejectionNote
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }

        public Guid InvoiceDocumentId { get; set; }
        public InvoiceDocument InvoiceDocument { get; set; }

        public Guid DeliveryChallanId { get; set; }
        public DeliveryChallan DeliveryChallan { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string RejectionNumber { get; set; }
        public DateTime RejectionDate { get; set; }

        public string Reason { get; set; }
        public string Notes { get; set; }

        public Guid CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<RejectionNoteItems> Items { get; set; } = new List<RejectionNoteItems>();
    }

    public class RejectionNoteItems
    {
        public Guid Id { get; set; }

        public Guid RejectionNoteId { get; set; }
        public RejectionNote RejectionNote { get; set; }

        public Guid InvoiceItemId { get; set; }
        public InvoiceItem InvoiceItem { get; set; }

        public Guid DeliveryChallanItemId { get; set; }
        public DeliveryChallanItems DeliveryChallanItems { get; set; }

        public Guid? ProductId { get; set; }
        public Product Product { get; set; }

        public string ItemName { get; set; }
        public string Unit { get; set; }

        public decimal RejectedQty { get; set; }
        public string Reason { get; set; }
    }
}
