using SmartLedger.Application.Interfaces.IAppDb;
using SmartLedger.Domain.Entities;

namespace SmartLedger.Infrastructure.DataSeeding
{
    public class UserSeeding : JsonSeeder<User>
    {
        protected override string GetFileName() => "Users.json";
        protected override Task<bool> ExistsAsync(IAppDbContext context)
            => Task.FromResult(context.Users.Any());
        protected override Task AddRangeAsync(IAppDbContext context, List<User> items)
            => context.Users.AddRangeAsync(items);
    }
}
