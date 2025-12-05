using InvoicingAPI.Domain.Entities;

namespace InvoicingAPI.Application.DTO
{
    public class CreateInvoiceDto
    {
        public Guid CompanyId { get; set; }
        public Guid CustomerId { get; set; }

        public DocumentType DocumentType { get; set; }          // Invoice / Quotation

        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }

        public string ReferenceNumber { get; set; }
        public string PlaceOfSupply { get; set; }

        public string Notes { get; set; }
        public string TermsAndConditions { get; set; }

        public bool IsGstInclusive { get; set; }

        public decimal TransportCharges { get; set; }
        public decimal LoadingCharges { get; set; }


        public List<InvoiceItemDto> Items { get; set; } = new();
    }
}
