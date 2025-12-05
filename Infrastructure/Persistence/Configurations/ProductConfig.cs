using InvoicingAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoicingAPI.Infrastructure.Persistence.Configurations;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);

        builder.HasOne(x => x.Company)
               .WithMany(c => c.Products)
               .HasForeignKey(x => x.CompanyId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
