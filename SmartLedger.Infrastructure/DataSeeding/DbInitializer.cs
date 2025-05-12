using SmartLedger.Application.Interfaces.IDataSeeding;
using SmartLedger.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Infrastructure.DataSeeding
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext _context;
        private readonly IEnumerable<ISeeder> _seeders;

        public DbInitializer(AppDbContext context, IEnumerable<ISeeder> seeders)
        {
            _context = context;
            _seeders = seeders;
        }

        public async Task InitializeAsync()
        {
            await _context.Database.EnsureCreatedAsync(); // or MigrateAsync() if supported

            foreach (var seeder in _seeders)
            {
                await seeder.SeedAsync(_context);
            }
        }
    }


}
