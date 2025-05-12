using SmartLedger.Application.Interfaces.IAppDb;
using SmartLedger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Infrastructure.DataSeeding
{
    public class JournalEntrySeeding : JsonSeeder<JournalEntry>
    {
        protected override string GetFileName() => "JournalEntries.json";
        protected override Task<bool> ExistsAsync(IAppDbContext context)
            => Task.FromResult(context.JournalEntries.Any()); 
        protected override Task AddRangeAsync(IAppDbContext context, List<JournalEntry> items)
            => context.JournalEntries.AddRangeAsync(items);
    }
}
