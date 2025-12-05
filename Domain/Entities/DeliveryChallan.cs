using System;
using System.Collections.Generic;

namespace InvoicingAPI.Domain.Entities
{
    public enum DeliveryChallanType
    {
        Normal = 0,
        Replacement = 1
    }

    public enum DeliveryChallanStatus
    {
        Draft = 0,
        Final = 1,
        Cancelled = 2
    }

    public class DeliveryChallan
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }

        public Guid? InvoiceDocumentId { get; set; }   // nullable to allow pre-invoice deliveries
        public InvoiceDocument InvoiceDocument { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string ChallanNumber { get; set; }
        public DateTime ChallanDate { get; set; }

        public DeliveryChallanType Type { get; set; }
        public DeliveryChallanStatus Status { get; set; }

        // Transport info
        public string VehicleNumber { get; set; }
        public string TransporterName { get; set; }

        public string Notes { get; set; }

        public Guid CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<DeliveryChallanItems> Items { get; set; } = new List<DeliveryChallanItems>();
    }

    public class DeliveryChallanItems
    {
        public Guid Id { get; set; }
        public Guid DeliveryChallanId { get; set; }
        public DeliveryChallan DeliveryChallan { get; set; }

        public Guid InvoiceItemId { get; set; }          // tie directly to invoice line
        public InvoiceItem InvoiceItem { get; set; }

        public Guid? ProductId { get; set; }
        public Product Product { get; set; }

        public string ItemName { get; set; }
        public string Unit { get; set; }

        public decimal Quantity { get; set; }
        public string Remarks { get; set; }
    }
}
