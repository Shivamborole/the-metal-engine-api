namespace InvoicingAPI.Domain.Entities
{
    public enum NumberResetFrequency
    {
        Never = 0,
        Yearly = 1,
        Monthly = 2
    }

    public class InvoiceNumberSettings
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }

        // Final template:
        // Examples:
        // {PREFIX}{YYYY}{MM}{SEQ}{SUFFIX}
        // {PREFIX}{SEQ}{SUFFIX}
        // {PREFIX}{YYYY}-{SEQ}
        //public string Template { get; set; } = "{PREFIX}{YYYY}{SEQ}";

        public string Template { get; set; } = "{PREFIX}{YYYY}-{SEQ}{SUFFIX}";

        public string Prefix { get; set; } = "";
        public string Suffix { get; set; } = "";

        // Total digits of SEQ => e.g., 3 => 001, 002 etc.
        public int Padding { get; set; } = 3;

        public NumberResetFrequency ResetFrequency { get; set; } = NumberResetFrequency.Never;

        public int CurrentNumber { get; set; } = 0;   // Last used counter
        public int CurrentYear { get; set; } = DateTime.UtcNow.Year;
        public int CurrentMonth { get; set; } = DateTime.UtcNow.Month;

        public bool IsDefault { get; set; } = false;
    }



}
