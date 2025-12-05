using System;
using InvoicingAPI.Domain.Entities;

namespace InvoicingAPI.Application.DTO
{
    public class InvoiceListDto
    {
        public Guid Id { get; set; }

        public string InvoiceNumber { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string CustomerName { get; set; }

        public decimal TotalAmount { get; set; }

        public DocumentType DocumentType { get; set; }   // FIX 1

        public PaymentStatus? PaymentStatus { get; set; }  // FIX 2

        public DocumentStatus Status { get; set; }  // FIX 3
    }
}
