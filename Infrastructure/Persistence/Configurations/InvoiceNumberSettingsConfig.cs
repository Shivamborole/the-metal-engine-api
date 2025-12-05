using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InvoicingAPI.Domain.Entities;

public class InvoiceNumberSettingsConfig : IEntityTypeConfiguration<InvoiceNumberSettings>
{
    public void Configure(EntityTypeBuilder<InvoiceNumberSettings> builder)
    {
        builder.ToTable("InvoiceNumberSettings");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Prefix).HasMaxLength(20);
        builder.Property(i => i.Suffix).HasMaxLength(20);

        builder.HasOne<Company>()
               .WithMany()
               .HasForeignKey(i => i.CompanyId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
