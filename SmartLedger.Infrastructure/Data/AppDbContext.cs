using Microsoft.EntityFrameworkCore;
using SmartLedger.Application.Interfaces.IAppDb;
using SmartLedger.Domain.Entities;
using SmartLedger.Infrastructure.Config;


namespace SmartLedger.Infrastructure.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<JournalEntry> JournalEntries => Set<JournalEntry>();
        public DbSet<JournalEntryDetail> JournalEntryDetails => Set<JournalEntryDetail>();

        public DbSet<Account> Accounts => Set<Account>();

        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new JournalEntryConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
