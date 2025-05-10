using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using SmartLedger.Domain.Entities;
using SmartLedgerPL.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Entity = SmartLedger.Domain.Entities;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartLedgerPL.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReportExportPdfPage : Page
    {
        //public JournalEntry journalEntry;
        //public ICollection<JournalEntryDetail> details;
        public ReportExportPdfViewModel ViewModel { get; }

        public ReportExportPdfPage()
        {
            this.InitializeComponent();
            ViewModel = App.Services.GetRequiredService<ReportExportPdfViewModel>();
            this.DataContext = ViewModel;
        }
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    journalEntry = e.Parameter as Entity.JournalEntry;
        //    details = journalEntry?. ?? new List<Reservation>();
        //    this.DataContext = this;
        //}
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.OnNavigatedTo(e.Parameter);
            
        }

    }
}
