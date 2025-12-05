using InvoicingAPI.Domain.Entities;
public class UserCompanyMap
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid CompanyId { get; set; }
    public Company Company { get; set; }

    public Guid RoleId { get; set; }      // Foreign Key
    public RoleMaster Role { get; set; }  // Navigation

    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}
