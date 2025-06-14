﻿using SmartLedger.Application.Interfaces.IAppDb;
using SmartLedger.Domain.Entities;

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
