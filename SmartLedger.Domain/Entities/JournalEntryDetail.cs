namespace SmartLedger.Domain.Entities
{
    public class JournalEntryDetail : BaseEntity.BaseEntity<long>
    {
        public long JournalEntryId { get; set; }
        public long AccountId { get; set; }
        public long DebitAmount { get; set; }
        public long CreditAmount { get; set; }
        public Account? Account { get; set; }
        public JournalEntry? JournalEntry { get; set; }
    }

}
