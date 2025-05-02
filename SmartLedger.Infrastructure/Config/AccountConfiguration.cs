using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SmartLedger.Domain.Entities;


namespace SmartLedger.Infrastructure.Config
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasMany(a => a.Details)
                   .WithOne(d => d.Account)
                   .HasForeignKey(d => d.AccountId);
        }
    }

}
