using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoicingAPI.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        // Multi-tenant
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Sku { get; set; }   // Optional, but useful

        [MaxLength(500)]
        public string Description { get; set; }

        [Required, MaxLength(20)]
        public string ProductType { get; set; }  // "Goods" | "Service" | "Composite" (string for now)

        [MaxLength(100)]
        public string Category { get; set; }

        [MaxLength(20)]
        public string HsnOrSac { get; set; }

        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SellingPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal GstRate { get; set; }   // 0,5,12,18,28
       

        public bool IsTaxInclusive { get; set; } = false;

        [Required, MaxLength(30)]
        public string SellingUnit { get; set; }  // Piece, Head, Part, KG, etc.

        public bool IsActive { get; set; } = true;
        public ICollection<ProductBOM> BOM { get; set; } = new List<ProductBOM>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
