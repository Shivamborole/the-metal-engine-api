using System;

namespace InvoicingAPI.Domain.Entities
{
    public class InvoiceItem
    {
        public Guid Id { get; set; }

        public Guid InvoiceDocumentId { get; set; }
        public InvoiceDocument InvoiceDocument { get; set; }

        // Optional: Linked Product
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }

        // Item Details
        public string ItemName { get; set; }          // snapshot of product name or manual
        public string HsnCode { get; set; }
        public string Unit { get; set; }              // PCS, KG, BOX etc.

        // Pricing & Tax
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }

        public decimal GstRate { get; set; }          // 0, 5, 12, 18, etc.
        public decimal GstAmount { get; set; }

        public decimal LineTotal { get; set; }        // Net amount including discount + GST

        // If user wants to save "custom item" directly into Product Master
        public bool SaveAsProduct { get; set; }
    }
}
