using SmartLedger.Application.DTOs;
using SmartLedger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Application.Interfaces.IServices
{
    public interface IJournalService
    {
        Task AddEntryAsync(JournalEntry entryIn);
        Task UpdateEntryAsync(JournalEntry entryIn);
        Task DeleteEntryAsync(long entryId);
        Task<List<JournalEntry>> GetAllPaginatedEntriesAsync(int pageNumber);
        Task<List<JournalEntry>> GetAllEntriesAsync();
        Task<int> GetCountEntriesAsync();
        Task<JournalEntry?> GetEntryByIdAsync(long entryId);
        Task SaveEntry();
        public void ClearEntry();

    }
}
