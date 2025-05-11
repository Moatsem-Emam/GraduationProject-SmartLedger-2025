using SmartLedger.Domain.Entities;


namespace SmartLedger.Domain.Interfaces.IRepositories
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category);
        Task<List<Category>> GetAllPaginatedAsync();
        Task<Category> GetByIdAsync(int id);
    }
}
