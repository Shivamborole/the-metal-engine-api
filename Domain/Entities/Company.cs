namespace InvoicingAPI.Domain.Entities
{

    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string GSTNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid OwnerUserId { get; set; }

        // Navigation
        //public bool IsActive { get; set; }
        public User OwnerUser { get; set; }
        public ICollection<UserCompanyMap> Users { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<Product> Products { get; set; }
       // public ICollection<Invoice> Invoices { get; set; }
        public ICollection<InvoiceDocument> Invoices { get; set; } = new List<InvoiceDocument>();
        public ICollection<Material> Materials { get; set; }
        //public ICollection<Product> Products { get; set; }
    }
}
