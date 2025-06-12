using SmartLedger.Application.Interfaces.IAppDb;

namespace SmartLedger.Application.Interfaces.IDataSeeding
{
    public interface ISeeder
    {
        Task SeedAsync(IAppDbContext context);
    }

}
