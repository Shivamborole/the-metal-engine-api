using InvoicingAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoicingAPI.Infrastructure.Persistence.Configurations;

public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(x => x.CustomerName).IsRequired().HasMaxLength(200);

        builder.HasOne(x => x.Company)
               .WithMany(c => c.Customers)
               .HasForeignKey(x => x.CompanyId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
