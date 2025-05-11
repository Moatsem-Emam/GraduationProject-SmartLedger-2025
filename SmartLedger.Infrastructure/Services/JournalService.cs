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

        public async Task AddEntryAsync(JournalEntry entryIn)
        {
            await _unitOfWork.LedgerRepository.AddAsync(entryIn);
            //await _unitOfWork.SaveAsync();
            
        }

        public async Task<List<JournalEntry>> GetAllPaginatedEntriesAsync(int pageNumber)
        {
           return await _unitOfWork.LedgerRepository.GetAllPaginatedAsync(pageNumber);
            
        }
        public async Task<int> GetCountEntriesAsync()
        {
            return await _unitOfWork.LedgerRepository.GetCountAsync();
        }

        public async Task<JournalEntry> GetEntryByIdAsync(long entryId)
        {
            return await _unitOfWork.LedgerRepository.GetByIdAsync(entryId);

        }

        public async Task SaveEntry()
        {
            await _unitOfWork.SaveAsync();
        }
        public void ClearEntry()
        {
            _unitOfWork.ClearContext();
        }

        public async Task UpdateEntryAsync(JournalEntry entryIn)
        {
            await _unitOfWork.LedgerRepository.UpdateAsync(entryIn);
        }

        public async Task DeleteEntryAsync(long entryId)
        {
            await _unitOfWork.LedgerRepository.DeleteAsync(entryId);
        }

        public async Task<List<JournalEntry>> GetAllEntriesAsync()
        {
            return await _unitOfWork.LedgerRepository.GetAllAsync();

        }
    }
}
