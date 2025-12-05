namespace InvoicingAPI.Domain.Entities
{
    public class RoleMaster
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool IsSystemRole { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
