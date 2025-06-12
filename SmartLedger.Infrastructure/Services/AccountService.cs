using SmartLedger.Application.Interfaces.IServices;
using SmartLedger.Domain.Entities;
using SmartLedger.Domain.Interfaces.IUnitOfWork;


namespace SmartLedger.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddAccountAsync(Account accountIn)
        {
            await _unitOfWork.AccountRepository.AddAsync(accountIn);
        }
        public async Task<List<Account>> GetAllAccountsAsync()
        {
            var accounts = await _unitOfWork.AccountRepository.GetAllPaginatedAsync();
            return accounts;
        }
        public async Task<Account?> GetAccountByIdAsync(int accountId)
        {
            return await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
        }
        public async Task SaveAccount()
        {
            await _unitOfWork.SaveAsync();
        }
        public void ClearAccount()
        {
            _unitOfWork.ClearContext();
        }
    }
}
