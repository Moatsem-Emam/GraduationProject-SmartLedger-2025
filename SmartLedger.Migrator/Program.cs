using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartLedger.Infrastructure.Data;

namespace SmartLedger.Migrator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            var provider = services.BuildServiceProvider();
            var context = provider.GetRequiredService<AppDbContext>();

            Console.WriteLine("Migration context ready.");
        }   
    }
}
