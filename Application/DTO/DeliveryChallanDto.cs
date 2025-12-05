using InvoicingAPI.Domain.Entities;

namespace InvoicingAPI.Application.DTO
{
    public class DeliveryChallanItemRequest
    {
        public Guid InvoiceItemId { get; set; }
        public decimal Quantity { get; set; }
        public string Remarks { get; set; }
    }

    public class CreateDeliveryChallanRequest
    {
        public Guid CompanyId { get; set; }
        public Guid InvoiceDocumentId { get; set; }
        public Guid CustomerId { get; set; }

        public DeliveryChallanType Type { get; set; }
        public DateTime ChallanDate { get; set; }

        public string VehicleNumber { get; set; }
        public string TransporterName { get; set; }
        public string Notes { get; set; }

        public Guid CreatedByUserId { get; set; }

        public List<DeliveryChallanItemRequest> Items { get; set; } = new();
    }

}
