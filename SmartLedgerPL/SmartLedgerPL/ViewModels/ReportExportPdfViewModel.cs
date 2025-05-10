using CommunityToolkit.Mvvm.ComponentModel;
using SmartLedger.Application.DTOs;
using SmartLedger.Application.Interfaces.IServices;
using SmartLedger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using SmartLedgerPL.Helpers;


namespace SmartLedgerPL.ViewModels
{
    public partial class ReportExportPdfViewModel: ObservableObject
    {
        [ObservableProperty]
        private JournalEntryDto selectedJournal;

        [ObservableProperty]
        private ICollection<ReportDto> reportData;

        [ObservableProperty]
        private List<ReportDto> reportDataList;

        [ObservableProperty]
        private long totalDebit;

        [ObservableProperty]
        private long totalCredit;

        [ObservableProperty]
        private List<long> allDebits;

        [ObservableProperty]
        private List<long> allCredits;

        [ObservableProperty]
        private ObservableCollection<ReportDto> deductions;

        [ObservableProperty]
        private ObservableCollection<ReportDto> entitlements;
        //public ICommand ExportToPdfCommand { get; }
        private readonly IJournalService _journalService;
        private readonly HelperUtilities _helper;
        public ReportExportPdfViewModel(IJournalService journalService,HelperUtilities helper)
        {
            _journalService = journalService;
            _helper = helper;
            //ExportToPdfCommand = new RelayCommand(ExportToPdf);
        }

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is JournalEntryDto entry)
            {
                //if (entry.Details is null) return;
                SelectedJournal = entry;

                // Load JournalEntryDetails from DB if needed
                (AllCredits,AllDebits,TotalCredit,TotalDebit) = _helper.GetDebitCreditSums(entry.Details);

                // قائمة الاستقطاعات (Debit فقط > 0)
                var deduction = entry.Details
                                .Where(d => d.DebitAmount > 0)
                                .Select(detail => new ReportDto
                                {
                                    AccountId = detail.AccountId,
                                    AccountName = detail.Account.AccountName,
                                    DebitAmount = detail.DebitAmount
                                });
                Deductions = new ObservableCollection<ReportDto>(deduction);

                // قائمة الاستحقاقات (Debit فقط > 0)
                var entitlement = entry.Details
                                .Where(d => d.CreditAmount > 0)
                                .Select(detail => new ReportDto
                                {
                                    AccountId = detail.AccountId,
                                    AccountName = detail.Account.AccountName,
                                    CreditAmount = detail.CreditAmount
                                });
                Entitlements = new ObservableCollection<ReportDto>(entitlement);
            }
        }


        //private void ExportToPdf()
        //{
        //    // هنا ممكن تستخدم مكتبة QuestPDF أو PdfSharpCore
        //    PdfGeneratorService.GenerateJournalPdf(SelectedJournal);
        //}
    }

}
