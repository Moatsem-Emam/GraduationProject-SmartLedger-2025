using Microsoft.Identity.Client;
using SmartLedger.Application.DTOs;
using SmartLedger.Application.Interfaces.IServices;
using SmartLedger.Domain.Entities;
using SmartLedger.Domain.Interfaces.IUnitOfWork;
using SmartLedger.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddCategoryAsync(Category CategoryIn)
        {
            await _unitOfWork.CategoryRepository.AddAsync(CategoryIn);
        }
        public async Task<List<Category>> GetAllCategorysAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllPaginatedAsync();
            return categories;
        }
        public async Task<Category?> GetCategoryByIdAsync(int CategoryId)
        {
            return await _unitOfWork.CategoryRepository.GetByIdAsync(CategoryId);
        }
        public async Task SaveCategory()
        {
            await _unitOfWork.SaveAsync();
        }
        public void ClearCategory()
        {
            _unitOfWork.ClearContext();
        }
    }
}
