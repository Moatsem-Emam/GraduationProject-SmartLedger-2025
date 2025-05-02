using Microsoft.EntityFrameworkCore;
using SmartLedger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartLedger.Application.Interfaces.IAppDb
{
    public interface IAppDbContext
    {
        DbSet<JournalEntry> JournalEntries { get; }
        DbSet<JournalEntryDetail> JournalEntryDetails { get; }
        DbSet<Account> Accounts { get; }
        DbSet<Category> Categories { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }

}
