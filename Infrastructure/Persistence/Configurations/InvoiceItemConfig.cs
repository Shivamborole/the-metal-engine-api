using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InvoicingAPI.Domain.Entities;

namespace InvoicingAPI.Infrastructure.Persistence.Configurations
{
    public class InvoiceItemConfig : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.ToTable("InvoiceItems");

            builder.HasKey(x => x.Id);

            // Numeric precision
            builder.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(x => x.Quantity).HasColumnType("decimal(18,2)");
            builder.Property(x => x.GstRate).HasColumnType("decimal(5,2)");
            builder.Property(x => x.GstAmount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.DiscountPercent).HasColumnType("decimal(5,2)");
            builder.Property(x => x.DiscountAmount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.LineTotal).HasColumnType("decimal(18,2)");

            // Product relationship (ProductId is nullable)
            builder.HasOne(x => x.Product)
                   .WithMany() // If you add ICollection<InvoiceItem> in Product later, update here
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.SetNull);

            // InvoiceDocument relationship
            builder.HasOne(x => x.InvoiceDocument)
                   .WithMany(doc => doc.Items)
                   .HasForeignKey(x => x.InvoiceDocumentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
