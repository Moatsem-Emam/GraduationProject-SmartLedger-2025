using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SmartLedger.Domain.Entities;


namespace SmartLedger.Infrastructure.Config
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            // Configure the primary key to not be auto-generated
            builder.Property(a => a.Id)
                   .ValueGeneratedNever();

            // Configure the one-to-many relationship
            builder.HasMany(a => a.Details)
                   .WithOne(d => d.Account)
                   .HasForeignKey(d => d.AccountId);

        }
    }

}
