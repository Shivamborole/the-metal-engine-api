namespace InvoicingAPI.Application.DTO
{
    public class CreateCustomerDto
    {
        public Guid CompanyId { get; set; }

        public string CustomerName { get; set; }
        public string CompanyName { get; set; }
        public string CustomerType { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string AlternatePhone { get; set; }

        public string GSTNumber { get; set; }
        public string PANNumber { get; set; }

        // Billing
        public string BillingAddressLine1 { get; set; }
        public string BillingAddressLine2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingPincode { get; set; }
        public string BillingCountry { get; set; }

        // Shipping
        public bool ShippingSame { get; set; }
        public string ShippingAddressLine1 { get; set; }
        public string ShippingAddressLine2 { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingPincode { get; set; }
        public string ShippingCountry { get; set; }

        public decimal? CreditLimit { get; set; }
        public decimal OpeningBalance { get; set; }
        public string Notes { get; set; }
    }
}
