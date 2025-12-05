using System;

namespace InvoicingAPI.Domain.Entities
{
    public class UnitConversion
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }

        public Guid? MaterialId { get; set; }
        public Guid? ProductId { get; set; }

        public string FromUnit { get; set; }
        public string ToUnit { get; set; }

        // Example: 1 Sheet = 3 Pieces → ConversionRate = 3
        public decimal ConversionRate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
