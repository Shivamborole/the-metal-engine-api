namespace InvoicingAPI.Application.DTO
{
    public class RejectionNoteItemRequest
    {
        public Guid DeliveryChallanItemId { get; set; }
        public decimal RejectedQty { get; set; }
        public string Reason { get; set; }
    }

    public class CreateRejectionNoteRequest
    {
        public Guid CompanyId { get; set; }
        public Guid InvoiceDocumentId { get; set; }
        public Guid DeliveryChallanId { get; set; }
        public Guid CustomerId { get; set; }

        public DateTime RejectionDate { get; set; }
        public string Reason { get; set; }
        public string Notes { get; set; }

        public Guid CreatedByUserId { get; set; }

        public List<RejectionNoteItemRequest> Items { get; set; } = new();
    }

}
