using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        DbSet<User> Users { get; }
        DbSet<PayrollItem> PayrollItems { get; }
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }

}
