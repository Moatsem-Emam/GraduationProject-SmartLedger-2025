using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Domain.Entities
{
    public class JournalEntry : BaseEntity.BaseEntity<long>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category? Category { get; set; } 
        public int UserId { get; set; }
        public User? User { get; set; }
        public ICollection<JournalEntryDetail> Details { get; set; } = new List<JournalEntryDetail>();
    }

}
