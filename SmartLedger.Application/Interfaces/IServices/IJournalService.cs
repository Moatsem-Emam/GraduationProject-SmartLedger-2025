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
        Task AddJournalEntryAsync(JournalEntry entryIn);
        Task<List<JournalEntryDto>> GetAllJournalEntriesAsync();
        Task<JournalEntry?> GetJournalEntryByIdAsync(int entryId);
        Task SaveJournalEntry();
        public void ClearJournalEntry();

    }
}
