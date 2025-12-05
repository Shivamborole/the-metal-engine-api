namespace InvoicingAPI.Application.DTO
{
    public class CreateProductDto
    {
        public Guid CompanyId { get; set; }

        public string Name { get; set; }
        public string Sku { get; set; }
        public string ProductType { get; set; }
        public string HsnOrSac { get; set; }
        public decimal GstRate { get; set; }
        public string SellingUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public string Category { get; set; }

        public bool IsActive { get; set; }

        public decimal SellingPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public bool IsTaxInclusive { get; set; }
        public string Description { get; set; }
    }
}

