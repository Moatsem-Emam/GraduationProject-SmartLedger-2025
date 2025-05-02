using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Application.Interfaces.IDTOs
{
    public interface IJournalEntryDetailDto
    {
        long AccountId { get; set; }
        public string AccountName { get; set; } 
        decimal DebitAmount { get; set; }
        decimal CreditAmount { get; set; }
    }
}
