using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoicingAPI.Domain.Entities
{
    public class Material
    {
        public Guid Id { get; set; }

        // Multi-tenant
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Code { get; set; }   // Internal code, optional

        [Required, MaxLength(30)]
        public string BaseUnit { get; set; }  // KG, SHEET, METER, LITER, etc.

        [Column(TypeName = "decimal(18,3)")]
        public decimal OpeningStockQty { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal CurrentStockQty { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal ReorderLevel { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AvgCost { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public decimal CurrentStock { get; internal set; }
    }
}
