using SmartLedgerPL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Drawing;
using System.Xml.XPath;
namespace SmartLedgerPL.ReportPdf
{
    public class ReportDocument : IDocument
    {
        private readonly ReportExportPdfViewModel vm;

        public ReportDocument(ReportExportPdfViewModel viewModel)
        {
            vm = viewModel;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(14).FontFamily("Arial"));

                page.Content().Column(col =>
                {
                    col.Spacing(15);

                    object value = col.Item().Text("الاستحقاقات").DirectionFromRightToLeft().SemiBold().FontSize(18).FontColor(Colors.Blue.Medium);

                    foreach (var item in vm.Entitlements)
                    {
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text(item.AccountId.ToString()).DirectionFromRightToLeft();
                            row.RelativeItem().Text(item.AccountName).DirectionFromRightToLeft();
                            row.ConstantItem(100).Text(item.EarningsAmount.ToString()).DirectionFromRightToLeft();
                        });
                        //.Background(Colors.Grey.Lighten3).Padding(5);
                    }

                    col.Item().Text($"إجمالي الاستحقاقات: {vm.TotalEarnings}").DirectionFromRightToLeft().Bold().FontColor(Colors.Blue.Darken2);

                    col.Item().Text("الاستقطاعات").DirectionFromRightToLeft().SemiBold().FontSize(18).FontColor(Colors.Red.Medium);

                    foreach (var item in vm.Deductions)
                    {
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text(item.AccountId.ToString()).DirectionFromRightToLeft();
                            row.RelativeItem().Text(item.AccountName).DirectionFromRightToLeft();
                            row.ConstantItem(100).Text(item.DeductionsAmount.ToString()).DirectionFromRightToLeft();
                        });
                        //.Background(Colors.Grey.Lighten3).Padding(5);
                    }
                    
                    col.Item().Text($"إجمالي الاستقطاعات: {vm.TotalDeductions}").DirectionFromRightToLeft().Bold().FontColor(Colors.Red.Darken2);

                    col.Item().Text("جملة الصافي المستحق:").DirectionFromRightToLeft().Bold().FontSize(16);
                    col.Item().Text(vm.Total.ToString()).DirectionFromRightToLeft().Bold().FontSize(20).FontColor(Colors.Green.Medium);

                    col.Item().Text("ملاحظات: ............").DirectionFromRightToLeft().FontSize(12);
                });
            });
        }
    }
}




