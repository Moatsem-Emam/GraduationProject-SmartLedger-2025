using SmartLedger.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Application.Interfaces.IDTOs
{
    public interface IJournalEntryDetailList
    {
        public List<JournalEntryDetailDto> Details { get; set; }

    }
}
