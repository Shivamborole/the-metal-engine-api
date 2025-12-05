namespace InvoicingAPI.Application.DTO
{
    public class UpdateCompanyDto
    {
        public string Name { get; set; }
        public string GSTNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
