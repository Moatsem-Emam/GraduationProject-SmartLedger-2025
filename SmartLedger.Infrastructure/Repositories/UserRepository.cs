

using SmartLedger.Application.Interfaces.IAppDb;
using SmartLedger.Domain.Entities;
using SmartLedger.Domain.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IAppDbContext _context;
        public UserRepository(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateAsync(string userName,string password)
        {
            var user = _context.Users.FirstOrDefault(u=>u.UserName== userName);
            if (user is null) return null;
            if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return user;
            return null;
        }
    }
}
