using SmartLedger.Application.DTOs;
using SmartLedger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Application.Interfaces.IServices
{
    public interface IAccountService
    {
        Task AddAccountAsync(Account accountIn);
        Task<List<Account>> GetAllAccountsAsync();
        Task<Account?> GetAccountByIdAsync(int accountId);
        Task SaveAccount();
        public void ClearAccount();
    }
}
