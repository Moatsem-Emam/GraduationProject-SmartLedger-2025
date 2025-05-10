using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartLedger.Domain.Interfaces.IRepositories;

namespace SmartLedger.Domain.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork
    {
        ILedgerRepository LedgerRepository { get; }
        IAccountRepository AccountRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IUserRepository UserRepository { get; }
        Task<int> SaveAsync();
        public void ClearContext();
    }
}
