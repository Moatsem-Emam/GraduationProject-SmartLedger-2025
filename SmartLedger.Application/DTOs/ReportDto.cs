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

        public long EarningsAmount { get; set; }
        public long DeductionsAmount { get; set; }
    }
}
