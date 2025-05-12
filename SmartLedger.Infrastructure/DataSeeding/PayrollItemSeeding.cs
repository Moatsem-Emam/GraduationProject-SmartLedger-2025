using SmartLedger.Application.Interfaces.IAppDb;
using SmartLedger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Infrastructure.DataSeeding
{
    public class PayrollItemSeeding : JsonSeeder<PayrollItem>
    {
        protected override string GetFileName() => "PayrollItems.json";
        protected override Task<bool> ExistsAsync(IAppDbContext context)
            => Task.FromResult(context.PayrollItems.Any());
        protected override Task AddRangeAsync(IAppDbContext context, List<PayrollItem> items)
            => context.PayrollItems.AddRangeAsync(items);
    }
}
