using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using SmartLedgerPL.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartLedgerPL.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReportExportPdfPage : Page
    {
        public ReportExportPdfViewModel ViewModel { get; }

        public ReportExportPdfPage()
        {
            this.InitializeComponent();
            ViewModel = App.Services.GetRequiredService<ReportExportPdfViewModel>();
            this.DataContext = ViewModel;
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.OnNavigatedTo(e.Parameter);
            
        }


        private async void ExportToPdfAsync(object sender, RoutedEventArgs e)
        {
            var filePicker = new Windows.Storage.Pickers.FileSavePicker();
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);

            filePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            filePicker.FileTypeChoices.Add("PDF Files", new List<string> { ".pdf" });
            filePicker.SuggestedFileName = "Report";

            var file = await filePicker.PickSaveFileAsync();
            if (file is null) return;

            if (DataContext is not ReportExportPdfViewModel vm) return;
            filePicker.SuggestedFileName = vm.Name;

            using var pdfStream = await file.OpenStreamForWriteAsync();

            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(20);
                    page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Arial"));

                    page.Header().Height(80).Background("#FFFFFF").Border(1).BorderColor("#E9ECEF").Row(headerRow =>
                    {

                        // Logo section (left side)
                        headerRow.ConstantItem(80).Padding(10).Column(logoCol =>
                        {
                            var logoStream = typeof(ReportExportPdfViewModel).Assembly
                                .GetManifestResourceStream("SmartLedgerPL.Assets.Images.SmartLedger_Light.png");

                            if (logoStream != null)
                            {
                                logoCol.Item().Width(60).Height(60).Image(logoStream).FitArea();
                            }
                            else
                            {
                                // Fallback if logo not found
                                logoCol.Item().Width(60).Height(60).Background("#17A2B8")
                                    .AlignCenter().AlignMiddle()
                                    .Text("LOGO").FontSize(12).Bold().FontColor("#FFFFFF");
                            }
                        });

                        // Header content (right side)
                        headerRow.RelativeItem().Padding(10).Column(headerContent =>
                        {
                            headerContent.Item().Row(topRow =>
                            {
                                topRow.RelativeItem().Text($"اليومية: {vm.Name ?? "غير محدد"}")
                                    .FontSize(14).Bold().FontColor("#2C3E50").AlignRight();
                            });

                        });
                    });

                    page.Content().Column(col =>
                    {
                        // Main content card with rounded corners and shadow effect
                        col.Item().Padding(10).Background("#F8F9FA").Border(1).BorderColor("#E9ECEF").Column(innerCol =>
                        {
                            // Two-column layout for Entitlements and Deductions
                            innerCol.Item().Row(row =>
                            {
                                // Right side - Entitlements (الاستحقاقات)
                                row.RelativeItem().Padding(10).Column(entCol =>
                                {
                                    entCol.Item().Text("الاستحقاقات")
                                        .FontSize(16).Bold().FontColor("#2C3E50").AlignCenter();

                                    entCol.Item().Height(10); // Spacing

                                    // Entitlements items
                                    foreach (var item in vm.Entitlements)
                                    {
                                        entCol.Item().Padding(5).Background("#FFFFFF").Border(1).BorderColor("#DEE2E6").Row(itemRow =>
                                        {
                                            // Amount on the left
                                            itemRow.ConstantItem(80).Text($"{item.EarningsAmount:N0}")
                                                .FontSize(12).FontColor("#2C3E50").AlignCenter();

                                            // Account name in the center
                                            itemRow.RelativeItem()
                                                .Text($"{item.AccountName}")
                                                .FontSize(11).FontColor("#6C757D").AlignCenter();

                                            // Account ID on the right
                                            itemRow.ConstantItem(80).Text(item.AccountId.ToString())
                                                .FontSize(11).FontColor("#6C757D").AlignCenter();
                                        });

                                        entCol.Item().Height(5); // Spacing between items
                                    }

                                    entCol.Item().Height(10); // Spacing before total

                                    // Total Entitlements
                                    entCol.Item().Row(totalRow =>
                                    {
                                        totalRow.RelativeItem().Text("إجمالي الاستحقاقات:")
                                            .FontSize(12).Bold().FontColor("#2C3E50").AlignRight();
                                    });

                                    entCol.Item().Text($"{vm.TotalEarnings:N0}")
                                        .FontSize(16).Bold().FontColor("#007BFF").AlignCenter();
                                });

                                // Left side - Deductions (الاستقطاعات)
                                row.RelativeItem().Padding(10).Column(dedCol =>
                                {
                                    dedCol.Item().Text("الاستقطاعات")
                                        .FontSize(16).Bold().FontColor("#2C3E50").AlignCenter();

                                    dedCol.Item().Height(10); // Spacing

                                    // Deductions items
                                    foreach (var item in vm.Deductions)
                                    {
                                        dedCol.Item().Padding(5).Background("#FFFFFF").Border(1).BorderColor("#DEE2E6").Row(itemRow =>
                                        {
                                            // Amount on the left
                                            itemRow.ConstantItem(80).Text($"{item.DeductionsAmount:N0}")
                                                .FontSize(12).FontColor("#2C3E50").AlignCenter();

                                            // Account name in the center
                                            itemRow.RelativeItem()
                                                .Text($"{item.AccountName}")
                                                .FontSize(11).FontColor("#6C757D").AlignCenter();

                                            // Account ID on the right
                                            itemRow.ConstantItem(80).Text(item.AccountId.ToString())
                                                .FontSize(11).FontColor("#6C757D").AlignCenter();
                                        });

                                        dedCol.Item().Height(5); // Spacing between items
                                    }

                                    dedCol.Item().Height(10); // Spacing before total

                                    // Total Deductions
                                    dedCol.Item().Row(totalRow =>
                                    {
                                        totalRow.RelativeItem().Text("إجمالي الاستقطاعات:")
                                            .FontSize(12).Bold().FontColor("#2C3E50").AlignRight();
                                    });

                                    dedCol.Item().Text($"{vm.TotalDeductions:N0}")
                                        .FontSize(16).Bold().FontColor("#007BFF").AlignCenter();
                                });
                            });
                        });

                        col.Item().Height(15); // Spacing between sections

                        // Blue gradient separator
                        col.Item().Height(8).Background("#4A90E2"); // Blue gradient effect

                        col.Item().Height(15); // Spacing

                        // Net Total Section (جملة الصافي المستحق)
                        col.Item().Padding(15).Background("#F8F9FA").Border(1).BorderColor("#E9ECEF").Column(totalCol =>
                        {
                            totalCol.Item().Text("جملة الصافي المستحق:")
                                .FontSize(14).Bold().FontColor("#2C3E50").AlignCenter();

                            totalCol.Item().Height(8); // Spacing

                            totalCol.Item().Text($"{vm.Total:N0}")
                                .FontSize(20).Bold().FontColor("#007BFF").AlignCenter();

                            totalCol.Item().Height(15); // Spacing

                            // Notes section
                            totalCol.Item().Text("ملاحظات: ............")
                                .FontSize(10).FontColor("#6C757D").AlignRight();
                        });
                    });
                });
            }).GeneratePdf(pdfStream);
        }

    }
}
