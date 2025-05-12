using SmartLedger.Application.Interfaces.IAppDb;
using SmartLedger.Application.Interfaces.IDataSeeding;
using SmartLedger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Infrastructure.DataSeeding
{
    public class AccountSeeding : JsonSeeder<Account>
    {
        protected override string GetFileName() => "Accounts.json";
        protected override Task<bool> ExistsAsync(IAppDbContext context)
            => Task.FromResult(context.Accounts.Any());
        protected override Task AddRangeAsync(IAppDbContext context, List<Account> items)
            => context.Accounts.AddRangeAsync(items);
    }
}
