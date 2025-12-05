using InvoicingAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoicingAPI.Infrastructure.Persistence.Configurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Phone).IsRequired(true);

        builder.HasIndex(x => x.Email).IsUnique();
    }
}
