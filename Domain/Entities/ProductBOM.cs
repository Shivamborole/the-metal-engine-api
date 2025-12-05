using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoicingAPI.Domain.Entities
{
    public class ProductBOM
    {
        public Guid Id { get; set; }

        // Multi-tenant (optional, but useful for quick filtering)
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid MaterialId { get; set; }
        public Material Material { get; set; }

        // How much material is required to produce 1 unit of product
        // e.g., 0.25 KG per lettuce head, 0.05 sheet per CNC part
        [Column(TypeName = "decimal(18,4)")]
        public decimal QtyRequiredPerUnit { get; set; }

        // Optional: % waste / scrap
        [Column(TypeName = "decimal(5,2)")]
        public decimal WastePercentage { get; set; }

        public bool IsActive { get; set; } = true;
        public decimal MaterialQuantity { get; internal set; }
    }
}
