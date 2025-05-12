using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SmartLedger.Application.DTOs;
using SmartLedger.Application.Interfaces.IDTOs;
using SmartLedger.Application.Interfaces.IServices;
using SmartLedger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartLedgerPL.Helpers
{
    public class HelperUtilities
    {
        private readonly ICategoryService _categoryService;

        private static readonly SemaphoreSlim _dialogLock = new SemaphoreSlim(1, 1);
        public NavigationView NavView => App.MainWindow.NavViewPublic;

        public HelperUtilities(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task ShowMessageDialogAsync(string message, string title)
        {

            await _dialogLock.WaitAsync();

            try
            {
                var dialog = new ContentDialog
                {
                    Title = title,
                    Content = message,
                    CloseButtonText = "تمام",
                    DefaultButton = ContentDialogButton.Close,
                    XamlRoot = App.MainWindow.Content.XamlRoot // Required in WinUI 3
                };

                await dialog.ShowAsync();
            }
            finally
            {
                _dialogLock.Release();
            }
        }

        public async Task<bool> ShowConfirmationDialogAsync(string message, string primaryBtn, string closeBtn)
        {
            await _dialogLock.WaitAsync();
            try
            {
                var dialog = new ContentDialog
                {
                    Title = "تأكيد",
                    Content = message,
                    PrimaryButtonText = primaryBtn,
                    CloseButtonText = closeBtn,
                    DefaultButton = ContentDialogButton.Primary,
                    XamlRoot = App.MainWindow.Content.XamlRoot
                };

                var result = await dialog.ShowAsync();
                return result == ContentDialogResult.Primary;
            }
            finally
            {
                _dialogLock.Release();
            }
        }

        public (List<long> Credits, List<long> Debits, long TotalCredit, long TotalDebit) GetDebitCreditSums(IEnumerable<JournalEntryDetail> entries)
        {
            var debits = entries.Select(e => e.DebitAmount).ToList();
            var credits = entries.Select(e => e.CreditAmount).ToList();
            var totalDebit = debits.Sum();
            var totalCredit = credits.Sum();

            return (credits, debits, totalCredit, totalDebit);
        }
        public enum Amount
        {
            Earnings,
            Deduction
        }

        public long SumAmount(IEnumerable<ReportDto> amountList, Amount amountType)
        {
          
            if (amountList == null || !amountList.Any())
                return 0;

            return (amountType == Amount.Earnings)
                ? amountList.Sum(a => a?.EarningsAmount ?? 0)
                : amountList.Sum(a => a?.DeductionsAmount ?? 0);
        }

        //public async Task<(bool confirm, JournalEntry editedEntries)> ShowTextEntryDialogAsync(JournalEntryDto entry)
        //{
        //    // Fetch categories from the service
        //    List<Category> categories = await _categoryService.GetAllCategorysAsync();

        //    // Create the input controls
        //    TextBox nameBox = new TextBox { PlaceholderText = "اليومية" };
        //    nameBox.Text = entry.Name;
        //    string Name = nameBox.Text;

        //    TextBox descriptionBox = new TextBox { PlaceholderText = "البيان", AcceptsReturn = true, Height = 20 };
        //    descriptionBox.Text = entry.Description;
        //    string Description = descriptionBox.Text;

        //    ComboBox categoryBox = new ComboBox
        //    {
        //        PlaceholderText = "الفئة",
        //        ItemsSource = categories,
        //        DisplayMemberPath = "CategoryName",  // Adjust according to your Category model
        //        SelectedValuePath = "Id",
        //        SelectedItem = entry.Category
        //    };
        //    Category Category = categoryBox.SelectedItem as Category;
        //    // Layout
        //    StackPanel panel = new StackPanel { Spacing = 10 };
        //    panel.Children.Add(nameBox);
        //    panel.Children.Add(descriptionBox);
        //    panel.Children.Add(categoryBox);

        //    // Dialog
        //    var dialog = new ContentDialog
        //    {
        //        Title = "تعديل",
        //        Content = panel,
        //        PrimaryButtonText = "Save",
        //        CloseButtonText = "Cancel",
        //        DefaultButton = ContentDialogButton.Primary,
        //        XamlRoot = App.MainWindow.Content.XamlRoot
        //    };

        //    var result = await dialog.ShowAsync();

        //    string EditedName = nameBox.Text;
        //    string EditedDescription = descriptionBox.Text;
        //    Category selectedCategory = categoryBox.SelectedItem as Category;
        //    string categoryName = selectedCategory?.CategoryName ?? "None";
        //    int categoryId = selectedCategory.Id;

        //    var editedEntries = new JournalEntry
        //    {
        //        Id = entry.Id,
        //        Name = EditedName,
        //        Description = EditedDescription,
        //        Category = selectedCategory,
        //        CategoryId = categoryId
        //    };



        //    if ((result == ContentDialogResult.Primary) && (Name == EditedName && Description == EditedDescription&&Category==selectedCategory))
        //        return (false, editedEntries);

        //    else if (result == ContentDialogResult.Primary)
        //        return (true, editedEntries); 

        //    return (false, editedEntries);
        //}

        public async Task<(bool confirm, JournalEntry editedEntries)> ShowTextEntryDialogAsync(JournalEntryDto entry)
        {
            await _dialogLock.WaitAsync();

            try
            {
                List<Category> categories = await _categoryService.GetAllCategorysAsync();

                // اسم اليومية
                var nameRow = new Grid { ColumnDefinitions = { new ColumnDefinition { Width = GridLength.Auto }, new ColumnDefinition() }, Margin = new Thickness(0, 5, 0, 0) };
                var nameBox = new TextBox { Text = entry.Name };
                var nameLabel = new TextBlock { Text = "اليومية:", VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 10, 0) };
                nameRow.Children.Add(nameBox);
                nameRow.Children.Add(nameLabel);
                Grid.SetColumn(nameLabel, 0);
                Grid.SetColumn(nameBox, 1);

                // البيان
                var descriptionLabel = new TextBlock { Text = "البيان:", VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(0, 0, 10, 0) };
                var descriptionRow = new Grid { ColumnDefinitions = { new ColumnDefinition { Width = GridLength.Auto }, new ColumnDefinition() }, Margin = new Thickness(0, 5, 0, 0) };
                var descriptionBox = new TextBox { Text = entry.Description, AcceptsReturn = true, Height = 80, TextWrapping = TextWrapping.Wrap };
                descriptionRow.Children.Add(descriptionBox);
                descriptionRow.Children.Add(descriptionLabel);
                Grid.SetColumn(descriptionLabel, 0);
                Grid.SetColumn(descriptionBox, 1);

                // الفئة
                var categoryLabel = new TextBlock { Text = "الفئة:", VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 10, 0) };
                var categoryRow = new Grid { ColumnDefinitions = { new ColumnDefinition { Width = GridLength.Auto }, new ColumnDefinition() }, Margin = new Thickness(0, 5, 0, 0) };
                var categoryBox = new ComboBox
                {
                    ItemsSource = categories,
                    DisplayMemberPath = "CategoryName",
                    SelectedValuePath = "Id",
                    SelectedItem = entry.Category
                };
                categoryRow.Children.Add(categoryBox);
                categoryRow.Children.Add(categoryLabel);
                Grid.SetColumn(categoryBox, 1);
                Grid.SetColumn(categoryLabel, 0);

                // الحاوية
                StackPanel panel = new StackPanel
                {
                    Spacing = 10,
                    Width = 400
                };
                panel.Children.Add(nameRow);
                panel.Children.Add(descriptionRow);
                panel.Children.Add(categoryRow);

                // صندوق الحوار
                var dialog = new ContentDialog
                {
                    Title = "تعديل",
                    Content = panel,
                    PrimaryButtonText = "حفظ",
                    CloseButtonText = "إلغاء",
                    DefaultButton = ContentDialogButton.Primary,
                    XamlRoot = App.MainWindow.Content.XamlRoot
                };

                var result = await dialog.ShowAsync();

                string EditedName = nameBox.Text;
                string EditedDescription = descriptionBox.Text;
                Category selectedCategory = categoryBox.SelectedItem as Category;

                var editedEntries = new JournalEntry
                {
                    Id = entry.Id,
                    Name = EditedName,
                    Description = EditedDescription,
                    Category = selectedCategory,
                    CategoryId = selectedCategory?.Id ?? 0
                };

                if ((result == ContentDialogResult.Primary) && (entry.Name == EditedName && entry.Description == EditedDescription && entry.Category?.Id == selectedCategory?.Id))
                    return (false, editedEntries);

                else if (result == ContentDialogResult.Primary)
                    return (true, editedEntries);

                return (false, editedEntries);
            }
            finally
            {
                _dialogLock.Release();
            }
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _categoryService.GetAllCategorysAsync();
        }

        public void FocusOn(string pageTag)
        {
            // عشان نفوكس علي الحاجة اللي اتنقلنا ليها
            var item = NavView.MenuItems
                       .OfType<NavigationViewItem>()
                       .FirstOrDefault(i => (string)i.Tag == pageTag);

            if (item != null)
            {
                NavView.SelectedItem = item;
                item.Focus(FocusState.Programmatic);
            }
        }
    }
}
