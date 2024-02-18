using Burile.Financial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Burile.Financial.Infrastructure.Data.Mappings;

public class PortfolioMap : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
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

        builder.Property(static x => x.Name)
               .HasMaxLength(256)
               .IsRequired(false);

        builder.Property(static x => x.IsRemoved)
               .IsRequired();

        // Table & Column Mappings
        builder.ToTable($"{nameof(Portfolio)}s");

        // Ignores

        // Indexes
        builder.HasIndex(static x => x.Id);
        builder.HasIndex(static x => x.ApiId);
        builder.HasIndex(static x => x.Name);
    }
}