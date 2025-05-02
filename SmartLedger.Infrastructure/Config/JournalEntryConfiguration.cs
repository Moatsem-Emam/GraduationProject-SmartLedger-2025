using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SmartLedger.Domain.Entities;


namespace SmartLedger.Infrastructure.Config
{
    public class JournalEntryConfiguration : IEntityTypeConfiguration<JournalEntry>
    {
        public void Configure(EntityTypeBuilder<JournalEntry> builder)
        {
            builder.HasMany(e => e.Details)
                   .WithOne(d => d.JournalEntry!)
                   .HasForeignKey(d => d.JournalEntryId);

            builder.HasOne(e => e.Category)
                   .WithMany(c => c.JournalEntries)
                   .HasForeignKey(e => e.CategoryId);
        }
    }

}
