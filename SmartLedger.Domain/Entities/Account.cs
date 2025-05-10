

namespace SmartLedger.Domain.Entities
{
    public class Account:BaseEntity.BaseEntity<long>
    {
        public string AccountName { get; set; }
        public ICollection<JournalEntryDetail> Details { get; set; } = new List<JournalEntryDetail>();

    }
}
    