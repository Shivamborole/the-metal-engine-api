namespace InvoicingAPI.Application.DTO
{
    public class InvoiceItemDto
    {
        public Guid? ProductId { get; set; }

        public string ItemName { get; set; }
        public string HsnCode { get; set; }
        public string Unit { get; set; }

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }

        public decimal GstRate { get; set; }
        public decimal GstAmount { get; set; }

        public decimal LineTotal { get; set; }

        public bool SaveAsProduct { get; set; }
    }


}
