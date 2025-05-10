using SmartLedger.Application.Interfaces.IDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Application.DTOs
{
    public class JournalEntryViewDto : IJournalEntryDetailDto,IJournalEntryDescDto
    {
        public int LineId { get; set; }
        public long AccountId { get; set; }
        public long DebitAmount { get; set; }
        public long CreditAmount { get; set; }
        public string Description { get; set; } = string.Empty;
        public string AccountName { get ; set; }
    }
}
