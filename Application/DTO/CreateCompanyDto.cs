namespace InvoicingAPI.Application.DTO
{
    public class CreateCompanyDto
    {

        public string Name { get; set; }
        public string GSTNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string IFSC { get; set; }
        public string BranchName { get; set; }
        public string UPIId { get; set; }
    }
}
