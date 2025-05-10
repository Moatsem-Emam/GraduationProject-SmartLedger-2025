using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartLedger.Application.DTOs;
using SmartLedger.Application.Interfaces.IServices;
using SmartLedger.Domain.Entities;
using SmartLedgerPL.Helpers;
using SmartLedgerPL.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using JournalEntry = SmartLedger.Domain.Entities.JournalEntry;

namespace SmartLedgerPL.ViewModels
{
    public partial class ReportPageViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IJournalService _journalService;
        private readonly HelperUtilities _helper;

        [ObservableProperty]
        private ObservableCollection<JournalEntryDto> journalEntries;

        [ObservableProperty]
        private bool isLoading;

        public ICommand NavigateToDetailsCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand LoadDataCommand { get; }
        public ReportPageViewModel(IJournalService journalService, INavigationService navigationService, HelperUtilities helper)
        {
            _journalService = journalService;
            _navigationService = navigationService;
            _helper = helper;
            JournalEntries = new ObservableCollection<JournalEntryDto>();
            NavigateToDetailsCommand = new RelayCommand<JournalEntryDto>(NavigateToDetails);
            EditCommand = new AsyncRelayCommand<JournalEntryDto>(UpdateJournalEntry);
            LoadDataCommand = new AsyncRelayCommand(LoadJournalEntriesAsync);
            DeleteCommand = new AsyncRelayCommand<JournalEntryDto>(DeleteJournalEntry);
            //LoadJournalEntriesAsync();
        }

        public async Task LoadJournalEntriesAsync()
        {
            IsLoading = true;

            try
            {
                var entries = await _journalService.GetAllJournalEntriesAsync();
                var entriesDto = entries.Select(e => new JournalEntryDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Category = e.Category,
                    CategoryName = e.Category?.CategoryName,
                    FormattedDate = e.CreatedAt.ToString("dddd, dd MMMM yyyy"),
                    Details = e.Details.Select(d => new JournalEntryDetail
                    {
                        Id = d.Id,
                        AccountId = d.AccountId,
                        Account = d.Account,
                        CreditAmount = d.CreditAmount,
                        DebitAmount = d.DebitAmount,
                    }).ToList(),
                }).ToList();

                entriesDto.Insert(0, new JournalEntryDto { IsAddNewCard = true });

                JournalEntries = new ObservableCollection<JournalEntryDto>(entriesDto);
            }
            finally
            {
                IsLoading = false;

            }
        }


        private void NavigateToDetails(JournalEntryDto entry)
        {
            var frame = App.MainRootFrame;
            frame.Navigate(typeof(ReportExportPdfPage), entry);
        }

        private async Task UpdateJournalEntry(JournalEntryDto journalEntry)
        {
            (bool confirm, JournalEntry entry) = await _helper.ShowTextEntryDialogAsync(journalEntry);
            if (confirm)
                await _journalService.UpdateJournalEntryAsync(entry);
            else return;
            try
            {
                await _journalService.SaveJournalEntry();
            }
            catch (Exception ex)
            {

                await _helper.ShowMessageDialogAsync(ex.Message, " خطأ في حفظ البيانات");
            }
            await LoadJournalEntriesAsync();
        }

        private async Task DeleteJournalEntry(JournalEntryDto journalEntry)
        {
            bool confirm = await _helper.ShowConfirmationDialogAsync("متأكد انك عايز تحذف اليومية دي؟" ,"حذف","الغاء");
            if (confirm)
            {
                long entryId = journalEntry.Id;
                await _journalService.DeleteJournalEntryAsync(entryId);
            }
            else return;
            try
            {
                await _helper.ShowMessageDialogAsync("تم الحذف بنجاح", "نجاح");
                await _journalService.SaveJournalEntry();
            }
            catch (Exception ex)
            {

                await _helper.ShowMessageDialogAsync(ex.Message, " خطأ في حذف اليومية");
            }
            await LoadJournalEntriesAsync();
        }

    }
}

