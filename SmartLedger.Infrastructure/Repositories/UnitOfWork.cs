using SmartLedger.Domain.Interfaces.IRepositories;
using SmartLedger.Domain.Interfaces.IUnitOfWork;
using SmartLedger.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public ILedgerRepository LedgerRepository { get; }
        public IAccountRepository AccountRepository { get; }
        public ICategoryRepository CategoryRepository { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            LedgerRepository = new LedgerRepository(_context);
            AccountRepository = new AccountRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void ClearContext()
        {
            _context.ChangeTracker.Clear();
        }
    }

}
