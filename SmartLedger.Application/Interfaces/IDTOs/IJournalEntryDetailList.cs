using SmartLedger.Application.DTOs;
using SmartLedger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Application.Interfaces.IDTOs
{
    public interface IJournalEntryDetailList
    {
        public IEnumerable<JournalEntryDetail> Details { get; set; }

    }
}
