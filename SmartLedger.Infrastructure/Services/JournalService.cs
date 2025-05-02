using SmartLedger.Application.DTOs;
using SmartLedger.Application.Interfaces.IServices;
using SmartLedger.Domain.Entities;
using SmartLedger.Domain.Interfaces.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Infrastructure.Services
{
    public class JournalService : IJournalService
    {
        private readonly IUnitOfWork _unitOfWork;

        public JournalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddJournalEntryAsync(JournalEntry entryIn)
        {
            await _unitOfWork.LedgerRepository.AddAsync(entryIn);
            //await _unitOfWork.SaveAsync();
            
        }

        public async Task<List<JournalEntryDto>> GetAllJournalEntriesAsync()
        {
            var entries = await _unitOfWork.LedgerRepository.GetAllAsync();
            return entries.Select(e => new JournalEntryDto
            {
                CreatedAt = e.CreatedAt,
                Description = e.Description,
                Details = e.Details.Select(d => new JournalEntryDetailDto
                {
                    AccountId = d.AccountId,
                    DebitAmount = d.DebitAmount,
                    CreditAmount = d.CreditAmount
                }).ToList()
            }).ToList();
        }

        public async Task<JournalEntry> GetJournalEntryByIdAsync(int entryId)
        {
            return await _unitOfWork.LedgerRepository.GetByIdAsync(entryId);

        }

        public async Task SaveJournalEntry()
        {
            await _unitOfWork.SaveAsync();
        }
        public void ClearJournalEntry()
        {
            _unitOfWork.ClearContext();
        }
    }
}
