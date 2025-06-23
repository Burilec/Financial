using Burile.Financial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Burile.Financial.Infrastructure.Data.Mappings;

public class MonthlyQuoteMap : IEntityTypeConfiguration<MonthlyQuote>
{
    public void Configure(EntityTypeBuilder<MonthlyQuote> builder)
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

        builder.Property(static x => x.DateTime)
               .IsRequired();

        builder.Property(static x => x.OpeningPrice)
               .IsRequired();

        builder.Property(static x => x.ClosingPrice)
               .IsRequired();

        builder.Property(static x => x.HighestPrice)
               .IsRequired();

        builder.Property(static x => x.LowestPrice)
               .IsRequired();

        builder.Property(static x => x.AdjustedClose)
               .IsRequired();

        builder.Property(static x => x.Volume)
               .IsRequired();

        builder.Property(static x => x.DividendAmount)
               .IsRequired();

        // Table & Column Mappings
        builder.ToTable($"{nameof(MonthlyQuote)}s");

        // Ignores

        // Indexes
        builder.HasIndex(static x => x.Id);
        builder.HasIndex(static x => x.ApiId);
        builder.HasIndex(static x => x.DateTime);
    }
}