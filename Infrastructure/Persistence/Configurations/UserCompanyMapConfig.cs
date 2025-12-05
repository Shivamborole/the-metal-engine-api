using InvoicingAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoicingAPI.Infrastructure.Persistence.Configurations
{
    public class UserCompanyMapConfig : IEntityTypeConfiguration<UserCompanyMap>
    {
        public void Configure(EntityTypeBuilder<UserCompanyMap> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // User relationship
            builder.HasOne(x => x.User)
                   .WithMany(u => u.UserCompanies)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Company relationship
            builder.HasOne(x => x.Company)
                   .WithMany(c => c.Users)
                   .HasForeignKey(x => x.CompanyId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Role relationship
            builder.HasOne(x => x.Role)
                   .WithMany()
                   .HasForeignKey(x => x.RoleId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
