namespace InvoicingAPI.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public ICollection<UserCompanyMap> UserCompanies { get; set; }
    }


}
