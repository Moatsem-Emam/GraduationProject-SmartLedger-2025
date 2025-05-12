using SmartLedger.Application.Interfaces.IAppDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Application.Interfaces.IDataSeeding
{
    public interface ISeeder
    {
        Task SeedAsync(IAppDbContext context);
    }

}
