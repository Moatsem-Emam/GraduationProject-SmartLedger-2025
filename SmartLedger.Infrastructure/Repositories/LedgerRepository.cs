using Azure;
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

        public async Task DeleteAsync(long entryId)
        {
            var entry = await _context.JournalEntries.FirstOrDefaultAsync(e=>e.Id == entryId);
            if (entry != null)
                _context.JournalEntries.Remove(entry);
            else return;
        }

        public async Task<List<JournalEntry>> GetAllPaginatedAsync(int pageNumber)
        {

            return await _context.JournalEntries
                .Include(e => e.Details)
                    .ThenInclude(d => d.Account)
                .Include(e => e.Category)
                .OrderByDescending(e => e.CreatedAt)
                .ThenByDescending(e => e.UpdatedAt)
                .Skip((pageNumber - 1) * (19))
                .Take(19)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.JournalEntries.CountAsync();

        }

        public async Task<JournalEntry> GetByIdAsync(long id)
        {
            return await _context.JournalEntries
                .Include(e => e.Details)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(JournalEntry entry)
        {
            var AttachedEntry = new JournalEntry()
            {
                Id = entry.Id,
                Category = entry.Category,
                CategoryId = entry.CategoryId,
                Name = entry.Name,
                Description = entry.Description,
                UpdatedAt = DateTime.UtcNow
            };


            // Attach the passed entity ONLY IF NOT TRACKED
            var local = _context.JournalEntries.Local.FirstOrDefault(e => e.Id == entry.Id);
            if (local != null)
            {
                // Detach the existing local instance
                _context.Entry(local).State = EntityState.Detached;
            }

            // Attach and mark properties as modified
            _context.JournalEntries.Attach(AttachedEntry);

            _context.Entry(AttachedEntry).Property(e => e.Name).IsModified = true;
            _context.Entry(AttachedEntry).Property(e => e.Description).IsModified = true;
            _context.Entry(AttachedEntry).Property(e => e.CategoryId).IsModified = true;
            _context.Entry(AttachedEntry).Property(e => e.UpdatedAt).IsModified = true;
        }

        public async Task<List<JournalEntry>> GetAllAsync()
        {
            return await _context.JournalEntries
                .Include(e => e.Details)
                    .ThenInclude(d => d.Account)
                .Include(e => e.Category)
                .OrderByDescending(e => e.CreatedAt)
                .ThenByDescending(e => e.UpdatedAt)
                .ToListAsync();
        }
    }
}

