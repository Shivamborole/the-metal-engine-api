using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoicingAPI.Domain.Entities
{
    public class MaterialPurchase
    {
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public Guid MaterialId { get; set; }
        public Material Material { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public DateTime PurchaseDate { get; set; }
        public DateTime CreatedAt { get; set; }
        [MaxLength(50)]
        public string ReferenceNumber { get; set; } // Purchase invoice no, etc.

        [MaxLength(250)]
        public string Notes { get; set; }
    }
}
