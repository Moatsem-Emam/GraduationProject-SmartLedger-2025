using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SmartLedger.Domain.Entities;


namespace SmartLedger.Infrastructure.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(c => c.JournalEntries)
                   .WithOne(e => e.Category)
                   .HasForeignKey(e => e.CategoryId);
        }
    }

}
