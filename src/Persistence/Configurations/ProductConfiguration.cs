using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(t => t.Name)
            .HasMaxLength(250);

        builder.Property(t => t.Description)
            .HasMaxLength(2000);

        builder.Property(t => t.Price)
            .HasPrecision(18, 2);
    }
}
