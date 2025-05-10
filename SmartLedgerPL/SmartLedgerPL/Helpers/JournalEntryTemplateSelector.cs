using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SmartLedger.Application.DTOs;

namespace SmartLedgerPL.Helpers
{
    public class JournalEntryTemplateSelector : DataTemplateSelector
    {
        public DataTemplate AddCardTemplate { get; set; }
        public DataTemplate EntryCardTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is JournalEntryDto dto && dto.IsAddNewCard)
                return AddCardTemplate;

            return EntryCardTemplate;
        }
    }

}
