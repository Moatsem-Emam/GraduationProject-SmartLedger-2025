using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Domain.Entities
{
    public class JournalEntry : BaseEntity.BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<JournalEntryDetail> Details { get; set; } = new List<JournalEntryDetail>();
    }

}
