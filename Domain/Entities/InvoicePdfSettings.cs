namespace InvoicingAPI.Domain.Entities
{
    public class InvoicePdfSettings
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }

        // Branding
        public string? CompanyDisplayName { get; set; }
        public string? CompanyAddressLine1 { get; set; }
        public string? CompanyAddressLine2 { get; set; }
        public string? CompanyGstin { get; set; }
        public string? CompanyPhone { get; set; }
        public string? CompanyEmail { get; set; }

        public string? LogoUrl { get; set; }          // later you can store blob / file

        // Bank details
        public string? BankName { get; set; }
        public string? AccountHolderName { get; set; }
        public string? AccountNumber { get; set; }
        public string? Ifsc { get; set; }

        // UPI / QR
        public string? UpiId { get; set; }            // e.g. farmwala@upi
        public string? UpiPayeeName { get; set; }     // e.g. MyFarmWala

        // Appearance
        public string PrimaryColorHex { get; set; } = "#2563EB"; // Tailwind blue

        // Footer / terms
        public string? TermsAndConditions { get; set; }
        public string? FooterText { get; set; }
    }
}
