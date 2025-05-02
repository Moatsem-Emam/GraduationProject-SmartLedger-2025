using SmartLedger.Application.Interfaces.IDTOs;
using SmartLedger.Domain.BaseEntity;


namespace SmartLedger.Application.DTOs
{
    public class JournalEntryDto : BaseEntity, IJournalEntryDto,IJournalEntryDetailList
    {
        public string Description { get  ; set ; } = string.Empty;
        public List<JournalEntryDetailDto> Details { get; set; } = new();
    }
}
