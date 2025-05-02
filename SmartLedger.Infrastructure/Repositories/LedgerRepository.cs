using Microsoft.EntityFrameworkCore;
using SmartLedger.Application.Interfaces.IAppDb;
using SmartLedger.Domain.Entities;
using SmartLedger.Domain.Interfaces.IRepositories;
using SmartLedger.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Infrastructure.Repositories
{
    public class LedgerRepository : ILedgerRepository
    {
        private readonly IAppDbContext _context;

        public LedgerRepository(IAppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(JournalEntry entry)
        {
            _context.JournalEntries.Add(entry);
        }
       
        public async Task<List<JournalEntry>> GetAllAsync()
        {
            return await _context.JournalEntries
                .Include(e => e.Details)
                .ToListAsync();
        }
        public async Task<JournalEntry> GetByIdAsync(int id)
        {
            return await _context.JournalEntries
                .Include(e => e.Details)
                .FirstOrDefaultAsync(e => e.Id == id);
        }


    }
}
