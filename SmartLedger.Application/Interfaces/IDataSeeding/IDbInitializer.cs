using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Application.Interfaces.IDataSeeding
{
    public interface IDbInitializer
    {
        Task InitializeAsync();
    }
}
