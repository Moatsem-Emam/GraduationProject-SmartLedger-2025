using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
//using Microsoft.VisualStudio.PlatformUI;
using SmartLedger.Application.DTOs;
using SmartLedger.Application.Interfaces.IServices;
using SmartLedger.Domain.Entities;
using SmartLedger.Domain.ValueObjects;
using SmartLedger.Infrastructure.Services;
using SmartLedgerPL.Helpers;

//using SmartLedgerPL.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace SmartLedgerPL.ViewModels
{
    public partial class JournalEntryViewModel : ObservableObject
    {
        private readonly IJournalService _journalService;
        private readonly IAccountService _accountService;
        private readonly ICategoryService _CategoryService;

        public IRelayCommand AddEntryCommand { get; }
        public IRelayCommand SaveCommand { get; }

        [ObservableProperty]
        private ObservableCollection<JournalEntryDetail> details = new();
       
        [ObservableProperty]
        private ObservableCollection<JournalEntryViewDto> journalEntries = new();

        [ObservableProperty]
        private ObservableCollection<Account> allAccounts = new();

        [ObservableProperty]
        private Account selectedAccount;

        [ObservableProperty]
        private ObservableCollection<Category> allCategories = new();

        [ObservableProperty]
        private Category selectedCategory;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string? debitAmount;

        [ObservableProperty]
        private string? creditAmount;

        [ObservableProperty]
        private bool isDebitEnabled = true;
        
        [ObservableProperty]
        private bool isCreditEnabled = true;

        [ObservableProperty]
        private bool isSaveEnabled = false;
        partial void OnDebitAmountChanged(string? value)
        {
            // لو المستخدم كتب في المدين، نقفل الدائن
            IsCreditEnabled = string.IsNullOrWhiteSpace(value);
        }

        partial void OnCreditAmountChanged(string? value)
        {
            // لو المستخدم كتب في الدائن، نقفل المدين
            IsDebitEnabled = string.IsNullOrWhiteSpace(value);
        }
       
        public JournalEntryViewModel(IJournalService journalService,IAccountService accountService, ICategoryService categoryService)
        {
            // Inject Services
            _journalService = journalService;
            _accountService = accountService;
            _CategoryService = categoryService;
            // Commands
            AddEntryCommand = new AsyncRelayCommand(AddEntryAsync);
            SaveCommand = new RelayCommand(Save);
            Details.CollectionChanged += Details_CollectionChanged;
            // Clear Context عشان لو فيه حاجة اتضافت بس معملتهاش سيف تشانجس
            ClearJournalEntry();
        }
        // Event Handler On Details Changes
        private void Details_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateSaveEnabledStatus();
        }
        private void UpdateSaveEnabledStatus()
        {
            long sumCredit = 0;
            long sumDebit = 0;

            foreach (var detail in Details)
            {
                if (detail.CreditAmount > 0)
                    sumCredit += detail.CreditAmount;
                else
                    sumDebit += detail.DebitAmount;
            }

            IsSaveEnabled = sumCredit == sumDebit;
        }

        private void ClearJournalEntry()
        {
            _journalService.ClearJournalEntry();
        }
        private async Task AddEntryAsync()
        {

            #region Before DDD
            //if ( !long.TryParse(CreditAmount, out long credit) & !long.TryParse(DebitAmount, out long debit) )
            //{
            //    // Invalid input
            //    await HelperUtilities.ShowMessageDialogAsync("دخل ارقام صحيحة!", "خطأ");
            //    return;
            //}
            //else if (credit == 0 && debit == 0) 
            //{
            //    await HelperUtilities.ShowMessageDialogAsync("لازم تحط قيم للمدين او للدائن علشان تقدر تضيف سطر!", "خطأ");
            //    return;
            //}
            //else if (Description is null)
            //{
            //    await HelperUtilities.ShowMessageDialogAsync("لازم تحط البيان علشان تقدر تضيف سطر!", "خطأ");
            //    return;
            //} 
            #endregion

            MoneyAmount creditAmount, debitAmount;
            Description description;

            try
            {
                creditAmount = new MoneyAmount(CreditAmount);
                debitAmount = new MoneyAmount(DebitAmount);
                description = new Description(Description);
            }
            catch (ArgumentException ex)
            {
                await HelperUtilities.ShowMessageDialogAsync(ex.Message, "خطأ");
                return;
            }

            if (creditAmount.IsZero && debitAmount.IsZero)
            {
                await HelperUtilities.ShowMessageDialogAsync("لازم تحط قيم للمدين او للدائن علشان تقدر تضيف سطر!", "خطأ");
                return;
            }

            // Continue with processing


            var detail = new JournalEntryDetail
            {
                
                AccountId = SelectedAccount.Id,
                DebitAmount = debitAmount.Value,
                CreditAmount = creditAmount.Value
            };
            Details.Add(detail);

            var journalEntry = new JournalEntryViewDto
            {
                AccountName = SelectedAccount.AccountName,
                DebitAmount = debitAmount.Value,
                CreditAmount = creditAmount.Value,
                Description = Description

            };
            JournalEntries.Add(journalEntry);
            //OnDetailsChanged(detail);
            // Reset inputs
            DebitAmount = string.Empty;
            CreditAmount = string.Empty;
        }
        private async void Save()
        {
            bool confirm = await (HelperUtilities.ShowConfirmationDialogAsync("Are You Sure To Save This Journal Entry?",
                "Save", "Cancel"));
            if (confirm)
            {

                var entry = new JournalEntry
                {
                    Name = Name,
                    Description = Description,
                    CategoryId = SelectedCategory.Id,
                    Details = Details.ToList(),
                };


                await _journalService.AddJournalEntryAsync(entry);
                await _journalService.SaveJournalEntry();

                Details.Clear();
                journalEntries.Clear();
                Description = string.Empty;
                IsSaveEnabled = false;
            }
            return;
        }

        public async Task LoadDataAsync()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            AllAccounts = new ObservableCollection<Account>(accounts);
            
            var categories = await _CategoryService.GetAllCategorysAsync();
            AllCategories = new ObservableCollection<Category>(categories);
        }

    }

}
