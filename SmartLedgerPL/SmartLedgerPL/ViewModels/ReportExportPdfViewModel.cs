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
using static SmartLedgerPL.Helpers.HelperUtilities;


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
        private long totalEarnings;

        [ObservableProperty]
        private long totalDeductions;

        [ObservableProperty]
        private long total;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string desc;

        [ObservableProperty]
        private string categoryName;

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
                Name = entry.Name;
                Desc = entry.Description;
                CategoryName = entry.CategoryName;
                // Load JournalEntryDetails from DB if needed
                //(AllCredits, AllDebits, TotalCredit, TotalDebit) = _helper.GetDebitCreditSums(entry.Details);
                // قائمة الاستحقاقات 
                var entitlement = entry.Details
                                .Where(d => d.Account.PayrollItemId == 1)
                                .Select(detail => new ReportDto
                                {
                                    AccountId = detail.AccountId,
                                    AccountName = detail.Account.AccountName,
                                    EarningsAmount = (detail.CreditAmount==0)? detail.DebitAmount: detail.CreditAmount
                                });
                TotalEarnings = _helper.SumAmount(amountList: entitlement, amountType: Amount.Earnings);
                Entitlements = new ObservableCollection<ReportDto>(entitlement);

                // قائمة الاستقطاعات 
                var deduction = entry.Details
                                .Where(d => d.Account.PayrollItemId == 2)
                                .Select(detail => new ReportDto
                                {
                                    AccountId = detail.AccountId,
                                    AccountName = detail.Account.AccountName,
                                    DeductionsAmount = (detail.CreditAmount == 0) ? detail.DebitAmount : detail.CreditAmount
                                    
                                });
                TotalDeductions = _helper.SumAmount(amountList: deduction, amountType: Amount.Deduction);
                Deductions = new ObservableCollection<ReportDto>(deduction);


                Total = TotalEarnings - TotalDeductions;
            }

        }


        //private void ExportToPdf()
        //{
        //    // هنا ممكن تستخدم مكتبة QuestPDF أو PdfSharpCore
        //    PdfGeneratorService.GenerateJournalPdf(SelectedJournal);
        //}
    }

}
