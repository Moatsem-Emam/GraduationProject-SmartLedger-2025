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
        internal readonly IJournalService _journalService;
        private readonly HelperUtilities _helper;

        // Observers
        [ObservableProperty]
        private ObservableCollection<JournalEntryDto> journalEntries;
        [ObservableProperty]
        private ObservableCollection<JournalEntryDto> filteredJournalEntries;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private bool isNull = false;

        [ObservableProperty]
        private bool paginationVisibility = true;
        //[ObservableProperty]
        //private List<int> pageNumbers;

        [ObservableProperty]
        private string searchText = ""; // Holds the search text
        //[ObservableProperty]
        //private int selectedPageSize;
        //[ObservableProperty]
        private int currentPageDisplay;

        //[ObservableProperty]
        internal int totalPages;

        //private int _currentPageDisplay;
        //public int CurrentPageDisplay
        //{
        //    get => _currentPageDisplay;
        //    set => SetProperty(ref _currentPageDisplay, value);
        //}

        // Pagination properties
        //private int _currentPage = 1;
        //public int CurrentPage
        //{
        //    get => _currentPage;
        //    set => SetProperty(ref _currentPage, value);
        //}

        //private int _totalPages;
        //public int TotalPages
        //{
        //    get => _totalPages;
        //    set
        //    {
        //        SetProperty(ref _totalPages, value);
        //        OnPropertyChanged(nameof(HasPreviousPage));
        //        OnPropertyChanged(nameof(HasNextPage));
        //        OnPropertyChanged(nameof(PageDisplay));
        //    }
        //}

        //public bool HasPreviousPage => CurrentPage > 1;
        //public bool HasNextPage => CurrentPage < TotalPages;
        [ObservableProperty]
        private string pageDisplay;

        private List<JournalEntryDto> allEntries = new();
        private List<JournalEntryDto> paginatedEntries = new();

        // Commands
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
            LoadDataCommand = new AsyncRelayCommand<int>(LoadPaginatedEntriesAsync);
            DeleteCommand = new AsyncRelayCommand<JournalEntryDto>(DeleteJournalEntry);
            //LoadPaginatedEntriesAsync();
        }

        public async Task LoadEntriesAsync()
        {
            var entries = await _journalService.GetAllEntriesAsync();
            await StandardizeEntries(entries);
            
        }
        public async Task LoadPaginatedEntriesAsync(int pageNumber = 1)
        {
            IsLoading = true;
            PaginationVisibility = true;
            currentPageDisplay = pageNumber;
            //if (pageNumber < 1 || pageNumber > TotalPages) return;
            try
            {
                //var totalCount = await _journalService.GetCountEntriesAsync();
                //totalPages = (int)Math.Ceiling((double)totalCount / 19); //19 = pageSize

                var entries = await _journalService.GetAllPaginatedEntriesAsync(pageNumber);
                paginatedEntries = await StandardizeEntries(entries);
                ApplyStandardizedEntries(paginatedEntries);
                await UpdatePaginationAsync();
                //PageNumbers = Enumerable.Range(1, totalPages).ToList();
                PageDisplay = $"الصفحة {currentPageDisplay} من {totalPages}";
                
                //FilteredJournalEntries = new ObservableCollection<JournalEntryDto>(JournalEntries);
                //if (FilteredJournalEntries is null) 
                //{
                //    IsNull = true;
                //    PaginationVisibility = !IsNull;

                //}

            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task InitializeAsync()
        {
            var entries = await _journalService.GetAllEntriesAsync();
            allEntries = await StandardizeEntries(entries);
            ApplyStandardizedEntries(allEntries);
            //await UpdatePaginationAsync();
        }
        private async Task UpdatePaginationAsync()
        {
            int totalCount = await _journalService.GetCountEntriesAsync();
            totalPages = (int)Math.Ceiling((double)totalCount / 19);
        }
        public async Task<List<JournalEntryDto>> StandardizeEntries(List<JournalEntry> entries)
        {
            var result = entries.Select(e => new JournalEntryDto
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

            return result;
        }
        private void ApplyStandardizedEntries(List<JournalEntryDto> entries)
        {
            entries.Insert(0, new JournalEntryDto { IsAddNewCard = true });
            JournalEntries = new ObservableCollection<JournalEntryDto>(entries);
            FilteredJournalEntries = new ObservableCollection<JournalEntryDto>(entries);
        }
        internal async Task FilterEntriesAsync()
        {
            IsLoading = true;
            try
            {
                await InitializeAsync();
                PaginationVisibility = false;
                IEnumerable<JournalEntryDto> filtered;

                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    // Always include the first item, and apply filter to the rest
                    //var first = JournalEntries.Take(1); // Keep the first item
                    filtered = JournalEntries
                        .Skip(1)
                        .Where(e => e.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

                }
                else
                {
                    await LoadPaginatedEntriesAsync();
                    filtered = JournalEntries;
                    PaginationVisibility = true;
                }

                FilteredJournalEntries = new ObservableCollection<JournalEntryDto>(filtered);
                IsNull = !filtered.Any(); // Check if the filtered (excluding first) has any match
                if (IsNull) PaginationVisibility = !IsNull;
            }
            finally
            {
                IsLoading = false;
            }
            
        }

        //internal void FilterEntriesAsync()
        //{
        //    IEnumerable<JournalEntryDto> filtered;

        //    if (!string.IsNullOrWhiteSpace(SearchText))
        //    {
        //        filtered = allEntries.Where(e =>
        //            e.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        //    }
        //    else
        //    {
        //        filtered = allEntries;
        //    }

        //    var filteredList = filtered.ToList();
        //    TotalPages = (int)Math.Ceiling((double)filteredList.Count / PageSize);

        //    if (CurrentPage > TotalPages) CurrentPage = TotalPages;
        //    if (CurrentPage < 1) CurrentPage = 1;

        //    var pagedData = filteredList
        //        .Skip((CurrentPage - 1) * PageSize)
        //        .Take(PageSize)
        //        .ToList();

        //    pagedData.Insert(0, new JournalEntryDto { IsAddNewCard = true });

        //    FilteredJournalEntries = new ObservableCollection<JournalEntryDto>(pagedData);
        //    IsNull = !filtered.Any();
        //    PaginationVisibility = !IsNull;

        //    PageDisplay = $"الصفحة {currentPageDisplay} من {totalPages}";
        //}


        private void NavigateToDetails(JournalEntryDto entry)
        {
            var frame = App.MainRootFrame;
            frame.Navigate(typeof(ReportExportPdfPage), entry);
        }

        private async Task UpdateJournalEntry(JournalEntryDto journalEntry)
        {
            (bool confirm, JournalEntry entry) = await _helper.ShowTextEntryDialogAsync(journalEntry);
            if (confirm)
                await _journalService.UpdateEntryAsync(entry);
            else return;
            try
            {
                await _journalService.SaveEntry();
            }
            catch (Exception ex)
            {

                await _helper.ShowMessageDialogAsync(ex.Message, " خطأ في حفظ البيانات");
            }
            if (!string.IsNullOrWhiteSpace(SearchText)) await FilterEntriesAsync();
            else await LoadPaginatedEntriesAsync();
        }

        private async Task DeleteJournalEntry(JournalEntryDto journalEntry)
        {
            bool confirm = await _helper.ShowConfirmationDialogAsync("متأكد انك عايز تحذف اليومية دي؟" ,"حذف","الغاء");
            if (confirm)
            {
                long entryId = journalEntry.Id;
                await _journalService.DeleteEntryAsync(entryId);
            }
            else return;
            try
            {
                await _helper.ShowMessageDialogAsync("تم الحذف بنجاح", "نجاح");
                await _journalService.SaveEntry();
            }
            catch (Exception ex)
            {

                await _helper.ShowMessageDialogAsync(ex.Message, " خطأ في حذف اليومية");
            }
            if (!string.IsNullOrWhiteSpace(SearchText)) await FilterEntriesAsync();
            else await LoadPaginatedEntriesAsync();
        }


    }
}

