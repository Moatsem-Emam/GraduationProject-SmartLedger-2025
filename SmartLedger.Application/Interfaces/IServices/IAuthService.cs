using SmartLedger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Application.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<User> Authenticate(string email, string password);
        //Task<User> AddUser(User user);
    }
}
