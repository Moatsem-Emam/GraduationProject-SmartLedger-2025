using SmartLedger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Domain.Interfaces.IRepositories
{
    public interface ILedgerRepository
    {
        Task AddAsync(JournalEntry entry);
        Task UpdateAsync(JournalEntry entry);
        Task DeleteAsync(long entryId);
        Task<List<JournalEntry>> GetAllPaginatedAsync(int pageNumber);
        Task<List<JournalEntry>> GetAllAsync();
        Task<int> GetCountAsync();
        Task<JournalEntry> GetByIdAsync(long id);
    }
}
