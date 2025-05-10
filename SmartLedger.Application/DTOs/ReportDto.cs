using SmartLedger.Application.Interfaces.IDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Application.DTOs
{
    public class ReportDto : IJournalEntryDetailDto
    {
        public long AccountId { get; set ; }
        public string? AccountName { get; set ; }
        public long DebitAmount { get; set; }
        public long CreditAmount { get; set; }
        public List<long> AllDebits { get; set; }
        public List<long> AllCredits { get; set; }
    }
}
