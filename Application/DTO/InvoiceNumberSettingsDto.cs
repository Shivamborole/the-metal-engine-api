using System;
using InvoicingAPI.Domain.Entities;

namespace InvoicingAPI.Application.DTO
{
    public class InvoiceNumberSettingsDto
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }

        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int Padding { get; set; }
        public NumberResetFrequency ResetFrequency { get; set; }

        // For display-only (no editing)
        public int CurrentNumber { get; set; }
        public int CurrentYear { get; set; }
        public int? CurrentMonth { get; set; }
    }

    public class UpdateInvoiceNumberSettingsDto
    {
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int Padding { get; set; }
        public NumberResetFrequency ResetFrequency { get; set; }
    }
}
