using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InvoicingAPI.Domain.Entities;
namespace InvoicingAPI.Application.Helpers
{
 

    public class InvoiceDocumentConfig : IEntityTypeConfiguration<InvoiceDocument>
    {
        public void Configure(EntityTypeBuilder<InvoiceDocument> builder)
        {
            builder.ToTable("InvoiceDocuments");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.InvoiceNumber)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(i => i.DocumentType)
                   .IsRequired();

            builder.Property(i => i.Status)
                   .IsRequired();

            builder.Property(i => i.SubTotal).HasColumnType("decimal(18,2)");
            builder.Property(i => i.TotalTax).HasColumnType("decimal(18,2)");
            builder.Property(i => i.TotalAmount).HasColumnType("decimal(18,2)");
            builder.Property(i => i.RoundOff).HasColumnType("decimal(18,2)");

            builder.HasOne(i => i.Customer)
                   .WithMany()
                   .HasForeignKey(i => i.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Company)
                   .WithMany()
                   .HasForeignKey(i => i.CompanyId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(i => i.Items)
                   .WithOne(it => it.InvoiceDocument)
                   .HasForeignKey(it => it.InvoiceDocumentId);
        }
    }

}
