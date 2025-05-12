

namespace SmartLedger.Domain.Entities
{
    public class Account:BaseEntity.BaseEntity<long>
    {
        public string? AccountName { get; set; }
        public int PayrollItemId { get; set; }
        public PayrollItem? PayrollItem { get; set; }
        public ICollection<JournalEntryDetail> Details { get; set; } = new List<JournalEntryDetail>();

    }
}
    