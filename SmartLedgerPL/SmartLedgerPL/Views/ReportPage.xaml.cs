using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using SmartLedger.Application.DTOs;
using SmartLedgerPL.Helpers;
using SmartLedgerPL.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartLedgerPL.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReportPage : Page
    {

        public ReportPageViewModel ViewModel { get; }

        private readonly HelperUtilities _helper;
        public NavigationView NavView => App.MainWindow.NavViewPublic;

        private int _currentPage = 1;
        private int _pageSize = 19;
        private int _totalPages;

        public ReportPage()
        {
            this.InitializeComponent();
            _helper = App.Services.GetRequiredService<HelperUtilities>();
            ViewModel = App.Services.GetRequiredService<ReportPageViewModel>();
            this.DataContext = ViewModel;
            //this.Loaded += ReportPage_Loaded;
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (e.ClickedItem is JournalEntryDto entry && entry.Details is null) return;

            var vm = DataContext as ReportPageViewModel;
            vm?.NavigateToDetailsCommand.Execute(e.ClickedItem);
        }
        private void AddEntry_Click(object sender, RoutedEventArgs e)
        {
            // انتقل لصفحة إنشاء قيد جديد أو افتح Dialog
            Frame.Navigate(typeof(JournalEntry));

            // عشان نفوكس علي الحاجة اللي اتنقلنا ليها
            _helper.FocusOn("JournalEntryPage");
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is JournalEntryDto journalEntry)
            {
                var vm = DataContext as ReportPageViewModel;
                vm?.EditCommand.Execute(journalEntry);

            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is JournalEntryDto journalEntry)
            {
                var vm = DataContext as ReportPageViewModel;
                vm?.DeleteCommand.Execute(journalEntry);

            }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            base.OnNavigatedTo(e);
            if (DataContext is ReportPageViewModel vm)
            {
                int totalCount = await ViewModel._journalService.GetCountEntriesAsync();
                _totalPages = (int)Math.Ceiling((double)totalCount / _pageSize);
                vm.LoadDataCommand.Execute(_currentPage);
                UpdatePaginationButtons();
            }
            //await InitializePaginationAsync();
        }
        private async void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                await ViewModel.LoadPaginatedEntriesAsync(_currentPage);
                UpdatePaginationButtons();
            }
        }

        private async void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                await ViewModel.LoadPaginatedEntriesAsync(_currentPage);
                UpdatePaginationButtons();
            }
        }

        private void UpdatePaginationButtons()
        {
            prev.IsEnabled = _currentPage > 1;
            next.IsEnabled = _currentPage < _totalPages;
        }

        //private async void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (DataContext is ReportPageViewModel viewModel)
        //    {
        //        viewModel.SearchText = SearchBox.Text;
        //        await viewModel.FilterEntriesAsync();
        //    }
        //}

        private CancellationTokenSource _cts;

        private async void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            try
            {
                await Task.Delay(300, _cts.Token); // 300ms delay
                if (DataContext is ReportPageViewModel viewModel)
                {
                    viewModel.SearchText = SearchBox.Text;
                    await viewModel.FilterEntriesAsync();
                }
            }
            
            catch (TaskCanceledException) { /* Ignored */ }
            finally
            {
                UpdatePaginationButtons();
            }
        }

    }
}