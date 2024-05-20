using Burile.Financial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Burile.Financial.Infrastructure.Data.Mappings;

public sealed class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Primary Key
        builder.HasKey(static x => x.Id);

        // Properties
        builder.Property(static x => x.Id)
               .ValueGeneratedOnAdd();

        builder.Property(static x => x.ApiId)
               .IsRequired();

        builder.Property(static x => x.CreatedAt)
               .IsRequired();

        builder.Property(static x => x.ModifiedAt)
               .IsRequired(false);

        builder.Property(static x => x.Symbol)
               .IsRequired();

        builder.Property(static x => x.Name)
               .HasMaxLength(256)
               .IsRequired(false);

        builder.Property(static x => x.Currency)
               .HasMaxLength(3)
               .IsRequired(false);

        builder.Property(static x => x.Exchange)
               .IsRequired(false);

        builder.Property(static x => x.Country)
               .IsRequired(false);

        builder.Property(static x => x.MicCode)
               .IsRequired(false);

        builder.Property(static x => x.IsRemoved)
               .IsRequired();

        builder.Property(static x => x.Track)
               .HasDefaultValue(false)
               .IsRequired();

        // Table & Column Mappings
        builder.ToTable($"{nameof(Product)}s");

        // Ignores

        // Indexes
        builder.HasIndex(static x => x.Id);
        builder.HasIndex(static x => x.ApiId);
        builder.HasIndex(static x => x.Symbol);
    }
}