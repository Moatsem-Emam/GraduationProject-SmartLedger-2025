using SmartLedger.Application.Interfaces.IAppDb;
using SmartLedger.Domain.Entities;
using SmartLedger.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Infrastructure.DataSeeding
{
    public class CategorySeeding : JsonSeeder<Category>
    {
        protected override string GetFileName() => "Categories.json";
        protected override Task<bool> ExistsAsync(IAppDbContext context)
            => Task.FromResult(context.Categories.Any());
        protected override Task AddRangeAsync(IAppDbContext context, List<Category> items)
            => context.Categories.AddRangeAsync(items);
    }

}
