using SmartLedger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedgerPL.Helpers
{
    public static class SessionManager
    {
        public static User? CurrentUser { get; private set; }

        public static void SetUser(User user) => CurrentUser = user;

        public static void Logout() => CurrentUser = null;

        public static bool IsInRole(string role)
            => CurrentUser?.Role == role;
    }

}
