using SmartLedger.Application.Interfaces.IDTOs;
using SmartLedger.Domain.BaseEntity;
using SmartLedger.Domain.Entities;


namespace SmartLedger.Application.DTOs
{
    public class JournalEntryDto : BaseEntity<long>, IJournalEntryDto, IJournalEntryDetailList
    {
        public string Name { get; set; }
        public string Description { get  ; set ; } = string.Empty;
        public string FormattedDate { get; set; }
        //public Account Account { get; set; }
        public IEnumerable<JournalEntryDetail> Details { get; set; }
        public Category? Category { get; set; }
        public string CategoryName { get ; set ; }
        public bool IsAddNewCard { get; set; } = false;
    }
}
