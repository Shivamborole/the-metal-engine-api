namespace InvoicingAPI.Application.DTO
{
    public class InvoiceDeliveryItemSummaryDto
    {
        public Guid InvoiceItemId { get; set; }
        public string ItemName { get; set; }
        public decimal InvoiceQty { get; set; }

        public decimal DeliveredQty { get; set; }          // Total dispatched (normal + replacement)
        public decimal RejectedQty { get; set; }
        public decimal EffectiveDeliveredQty { get; set; } // Delivered - Rejected
        public decimal PendingQty { get; set; }

        public decimal RemainingRejectionToReplace { get; set; }
    }

    public class InvoiceDeliverySummaryDto
    {
        public Guid InvoiceDocumentId { get; set; }

        public decimal TotalInvoiceQty { get; set; }
        public decimal TotalDeliveredQty { get; set; }          // all dispatches
        public decimal TotalRejectedQty { get; set; }
        public decimal TotalEffectiveDeliveredQty { get; set; } // Delivered - Rejected
        public decimal TotalPendingQty { get; set; }

        public List<InvoiceDeliveryItemSummaryDto> Items { get; set; } = new();
    }

}
