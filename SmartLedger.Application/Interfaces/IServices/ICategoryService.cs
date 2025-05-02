using SmartLedger.Application.DTOs;
using SmartLedger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Application.Interfaces.IServices
{
    public interface ICategoryService
    {
        Task AddCategoryAsync(Category CategoryIn);
        Task<List<Category>> GetAllCategorysAsync();
        Task<Category?> GetCategoryByIdAsync(int CategoryId);
        Task SaveCategory();
        public void ClearCategory();
    }
}
