using SmartLedger.Application.Interfaces.IServices;
using SmartLedger.Domain.Entities;
using SmartLedger.Domain.Interfaces.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public Task<User> AddUser(User user)
        //{
            
        //}

        public async Task<User> Authenticate(string email, string password)
        {
            return await _unitOfWork.UserRepository.AuthenticateAsync(email, password);
        }
    }
}
