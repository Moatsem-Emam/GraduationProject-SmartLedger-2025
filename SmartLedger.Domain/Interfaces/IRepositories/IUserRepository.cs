using SmartLedger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Domain.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string email, string password);

    }
}
