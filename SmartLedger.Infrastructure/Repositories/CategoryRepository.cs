using Microsoft.EntityFrameworkCore;
using SmartLedger.Application.Interfaces.IAppDb;
using SmartLedger.Domain.Entities;
using SmartLedger.Domain.Interfaces.IRepositories;


namespace SmartLedger.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly IAppDbContext _context;

        public CategoryRepository(IAppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Category category)
        {
              _context.Categories.Add(category);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories
                .Include(c=>c.JournalEntries)
                .ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c=>c.JournalEntries)
                .FirstOrDefaultAsync(c=>c.Id == id);
        }
    }
}
