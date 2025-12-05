using InvoicingAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoicingAPI.Infrastructure.Persistence.Configurations;

public class CompanyConfig : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);

        builder.HasOne(x => x.OwnerUser)
               .WithMany()
               .HasForeignKey(x => x.OwnerUserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
