using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoicingAPI.Domain.Entities
{
    public class StockTransaction
    {
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public Guid MaterialId { get; set; }
        public Material Material { get; set; }

        // Positive for inward, negative for outward
        [Column(TypeName = "decimal(18,3)")]
        public decimal QuantityChange { get; set; }

        [Required, MaxLength(30)]
        public string TransactionType { get; set; } // "Purchase", "Invoice", "Adjustment", "Production"

        [MaxLength(50)]
        public string SourceReference { get; set; }  // e.g., InvoiceNo or PurchaseNo

        public Guid? SourceId { get; set; }         // e.g., InvoiceId, PurchaseId

        [MaxLength(250)]
        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
