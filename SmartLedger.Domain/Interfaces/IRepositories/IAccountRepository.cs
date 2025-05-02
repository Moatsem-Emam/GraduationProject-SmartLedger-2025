using SmartLedger.Domain.Entities;

namespace SmartLedger.Domain.Interfaces.IRepositories
{
    public interface IAccountRepository
    {
        Task AddAsync(Account account);
        Task<List<Account>> GetAllAsync();
        Task<Account> GetByIdAsync(int id);
    }
}
