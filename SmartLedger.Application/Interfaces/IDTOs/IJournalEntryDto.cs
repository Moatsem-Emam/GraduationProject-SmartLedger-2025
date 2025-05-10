using SmartLedger.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Application.Interfaces.IDTOs
{
    public interface IJournalEntryDto
    {
        public string Name { get; set; }

        public string CategoryName { get; set; }
        
        public string FormattedDate { get; set; }
    }
}
