using Microsoft.EntityFrameworkCore;
using SmartLedger.Application.Interfaces.IAppDb;
using SmartLedger.Domain.Entities;
using SmartLedger.Domain.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Infrastructure.Repositories
{
    public class AccountRepository:IAccountRepository
    {
        private readonly IAppDbContext _context;

        public AccountRepository(IAppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Account account)
        {
            _context.Accounts.Add(account);
        }

        public async Task<List<Account>> GetAllPaginatedAsync()
        {
            return await _context.Accounts
                .Include(a => a.Details)
                .ToListAsync();
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            return await _context.Accounts
                .Include(a => a.Details)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
