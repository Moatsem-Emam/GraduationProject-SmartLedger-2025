using Microsoft.EntityFrameworkCore;
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
        protected override async Task<bool> ExistsAsync(IAppDbContext context)
            => await(context.Accounts.AnyAsync());
        protected override async Task AddRangeAsync(IAppDbContext context, List<Account> items)
            => await context.Accounts.AddRangeAsync(items);
    }
}
