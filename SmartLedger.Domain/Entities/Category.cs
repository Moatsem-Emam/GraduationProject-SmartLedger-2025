

namespace SmartLedger.Domain.Entities
{
    public class Category:BaseEntity.BaseEntity<int>
    {
        public string CategoryName { get; set; }
        public ICollection<JournalEntry> JournalEntries { get; set; } = new List<JournalEntry>();
    }
}
