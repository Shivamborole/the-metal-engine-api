using InvoicingAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MenuItemConfig : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.ToTable("MenuItems");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.Icon).IsRequired();
        builder.Property(x => x.Route).IsRequired();
        builder.Property(x => x.Order).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();

        // Do NOT use HasData here if the table already exists
    }
}
