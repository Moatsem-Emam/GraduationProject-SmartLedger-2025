using SmartLedger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Application.DTOs
{
    public class AccountDto:Account
    {
        public string AccountIdAndName { get; set; }
    }
}
