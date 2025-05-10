using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Microsoft.UI.Xaml.Controls;

using Microsoft.UI.Xaml;

//using Microsoft.VisualStudio.PlatformUI;
using SmartLedger.Application.DTOs;
using SmartLedger.Application.Interfaces.IServices;
using SmartLedger.Domain.Common;
using SmartLedger.Domain.Entities;
using SmartLedger.Domain.ValueObjects;
using SmartLedger.Infrastructure.Services;
using SmartLedgerPL.Helpers;
using SmartLedgerPL.Views;


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
using System.Windows.Controls;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using JournalEntry = SmartLedger.Domain.Entities.JournalEntry;

namespace SmartLedgerPL.ViewModels
{
    public partial class JournalEntryViewModel : ObservableObject
    {
        private readonly IJournalService _journalService;
        private readonly IAccountService _accountService;
        private readonly ICategoryService _CategoryService;
        private readonly HelperUtilities _helper;
        public IRelayCommand AddEntryCommand { get; }
        public IRelayCommand SaveCommand { get; }

        [ObservableProperty]
        private static int lineId;

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
       
        public JournalEntryViewModel(IJournalService journalService,IAccountService accountService, ICategoryService categoryService, HelperUtilities helper)
        {
            //App.MainWindow.Activate();
            // Inject Services
            _journalService = journalService;
            _accountService = accountService;
            _CategoryService = categoryService;
            _helper = helper;
            // Commands
            AddEntryCommand = new AsyncRelayCommand(AddEntryAsync);
            SaveCommand = new RelayCommand(Save);
            Details.CollectionChanged += Details_CollectionChanged;
            lineId = 0;
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
            //    await _helper.ShowMessageDialogAsync("دخل ارقام صحيحة!", "خطأ");
            //    return;
            //}
            //else if (credit == 0 && debit == 0) 
            //{
            //    await _helper.ShowMessageDialogAsync("لازم تحط قيم للمدين او للدائن علشان تقدر تضيف سطر!", "خطأ");
            //    return;
            //}
            //else if (Description is null)
            //{
            //    await _helper.ShowMessageDialogAsync("لازم تحط البيان علشان تقدر تضيف سطر!", "خطأ");
            //    return;
            //} 
            #endregion

            MoneyAmount creditAmount, debitAmount;
            Inputs<string> name;
            Inputs<string> description;
            Inputs<Account> account;
            Inputs<Category> category;

            try
            {
                creditAmount = new MoneyAmount(CreditAmount);
                debitAmount = new MoneyAmount(DebitAmount);
                name = new Inputs<string>(Name, LedgerInputs.Name);
                description = new Inputs<string>(Description,LedgerInputs.Description);
                account = new Inputs<Account>(SelectedAccount,LedgerInputs.Account);
                category = new Inputs<Category>(selectedCategory,LedgerInputs.Category);
            }
            catch (ArgumentException ex)
            {
                await _helper.ShowMessageDialogAsync(ex.Message, "خطأ");
                return;
            }

            if (creditAmount.IsZero && debitAmount.IsZero)
            {
                await _helper.ShowMessageDialogAsync("لازم تحط قيم للمدين او للدائن علشان تقدر تضيف سطر!", "خطأ");
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
                LineId = LineId + 5,
                AccountName = SelectedAccount.AccountName,
                DebitAmount = debitAmount.Value,
                CreditAmount = creditAmount.Value,
                Description = Description

            };
            LineId += 5;
            
            JournalEntries.Add(journalEntry);
            //OnDetailsChanged(detail);
            // Reset inputs
            DebitAmount = string.Empty;
            CreditAmount = string.Empty;
            
        }

        public NavigationView NavView => App.MainWindow.NavViewPublic;

        private async void Save()
        {
            bool confirm = await (_helper.ShowConfirmationDialogAsync("متأكد انك عايز تحجز الأموال؟",
                "حجز", "الغاء"));
            if (confirm)
            {
                var entry = new JournalEntry
                {
                    UserId = SessionManager.CurrentUser.Id,
                    Name = Name,
                    Description = Description,
                    CategoryId = SelectedCategory.Id,
                    Details = Details.ToList(),
                };


                await _journalService.AddJournalEntryAsync(entry);
                try
                {
                    await _journalService.SaveJournalEntry();
                }
                catch (Exception ex)
                {

                    await _helper.ShowMessageDialogAsync(ex.Message, " خطأ في حجز الاموال");
                    return;
                }
                await _helper.ShowMessageDialogAsync("تم حجز الأموال بنجاح!", "نجاح");


                Details.Clear();
                JournalEntries.Clear();
                Description = string.Empty;
                Name= string.Empty;
                SelectedAccount = null;
                SelectedCategory = null;
                IsSaveEnabled = false;
                lineId = 0;

                var frame = App.MainRootFrame;
                frame.Navigate(typeof(ReportPage));

                // عشان نفوكس علي الحاجة اللي اتنقلنا ليها
                var item =  NavView.MenuItems
                           .OfType<NavigationViewItem>()
                           .FirstOrDefault(i => (string)i.Tag == "ReportPage");

                if (item != null)
                {
                    NavView.SelectedItem = item;
                    item.Focus(FocusState.Programmatic);
                }

                NavView.SelectedItem = item;
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
