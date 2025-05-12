using SmartLedger.Application.Interfaces.IAppDb;
using SmartLedger.Application.Interfaces.IDataSeeding;
using SmartLedger.Infrastructure.Data;
using System.Text.Json;
namespace SmartLedger.Infrastructure.DataSeeding
{

    public abstract class JsonSeeder<T> : ISeeder where T : class
    {
        public async Task SeedAsync(IAppDbContext context)
        {
            if (await ExistsAsync(context)) return;

            string fileName = GetFileName();
            string basePath = AppContext.BaseDirectory;
            string path = Path.Combine(basePath, "Data", "Seed", fileName);

            if (!File.Exists(path)) return;

            var json = await File.ReadAllTextAsync(path);
            var data = JsonSerializer.Deserialize<List<T>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (data != null && data.Any())
            {
                await AddRangeAsync(context, data);
                await context.SaveChangesAsync(CancellationToken.None);
            }
        }

        protected abstract string GetFileName();
        protected abstract Task<bool> ExistsAsync(IAppDbContext context);
        protected abstract Task AddRangeAsync(IAppDbContext context, List<T> items);
    }


}
