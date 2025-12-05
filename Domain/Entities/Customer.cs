namespace InvoicingAPI.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }

        // Basic Info
        public string CustomerName { get; set; }
        public string CompanyName { get; set; }
        public string CustomerType { get; set; } // Individual / Business

        // Contact
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AlternatePhone { get; set; }

        // Tax info
        public string GSTNumber { get; set; }
        public string PANNumber { get; set; }

        // Billing Address
        public string BillingAddressLine1 { get; set; }
        public string BillingAddressLine2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingPincode { get; set; }
        public string BillingCountry { get; set; }

        // Shipping Address
        public bool ShippingSame { get; set; }
        public string ShippingAddressLine1 { get; set; }
        public string ShippingAddressLine2 { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingPincode { get; set; }
        public string ShippingCountry { get; set; }

        // Finance
        public decimal? CreditLimit { get; set; }
        public decimal OpeningBalance { get; set; }
        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation
        public Company Company { get; set; }
        public ICollection<InvoiceDocument> Invoices { get; set; } = new List<InvoiceDocument>();

    }
}
