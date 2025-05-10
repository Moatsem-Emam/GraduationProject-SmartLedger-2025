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
            //if (e.ClickedItem is JournalEntry entry)
            //{
            //}

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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (DataContext is ReportPageViewModel vm)
            {
                vm.LoadDataCommand.Execute(null);
            }
        }

    }
}
